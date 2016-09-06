using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpotifyMetadata
{
    public class ArtistRepository
    {
        SpotifyContext db = new SpotifyContext();
        ApiSpotify api = new ApiSpotify();
        public ArtistSearchResult SearchArtist(string query)
        {
            ArtistSearchResult result = new ArtistSearchResult();
            query = query.ToLower();

            //Separar la query entre palabras sueltas y frases entre comillas, para que se parezca al comportamiento de la API
            var queryParts = (from Match match in Regex.Matches(query, @"[\""].+?[\""]|[^ ]+")
                              select match.ToString()).ToList();


            foreach (var q in queryParts)
            {
                var part = q.Trim('"');
                result.DownloadedMatches.AddRange(db.Artists
                    .Where(a => a.Name.ToLower().Contains(part)).ToList<Artist>()); ;
            }
            var apiMatches = api.SearchArtist(query);
            var excludedIDs = new HashSet<string>(result.DownloadedMatches.Select(r => r.SpotifyId));
            result.NotDownloadedMatches = (from m in apiMatches
                                           where !excludedIDs.Contains(m.id)
                                           select new Artist { SpotifyId = m.id, Name = m.name, ImageUrl = m.MainImageUrl })
                                          .ToList();

            return result;
        }

        public Artist UpdateArtistInfo(int? id)
        {
            var artist = GetArtistById(id);
            if (artist != null)
            {
                artist = DownloadArtistInfo(artist.SpotifyId);
            }
            return artist;
        }

        public List<Artist> GetAllDownloadedArtists()
        {
            SearchArtist("\"arrows and\" sound");
            return db.Artists.ToList();
        }

        public Artist DownloadArtistInfo(string spotifyId)
        {
            var artistData = api.DownloadArtistFullData(spotifyId);
            var artistToSave = new Artist()
            {
                ImageUrl = artistData.MainImageUrl,
                Name = artistData.name,
                SpotifyId = artistData.id
            };
            artistToSave = SaveArtist(artistToSave);

            var albumsList = api.DownloadArtistAlbums(spotifyId);
            foreach (var albumItem in albumsList)
            {
                var albumData = api.DownloadAlbumFullData(albumItem.id);
                var albumToSave = new Album()
                {
                    ArtistId = artistToSave.Id,
                    ImageUrl = albumData.MainImageUrl,
                    Name = albumData.name,
                    Popularity = albumData.popularity,
                    SpotifyId = albumData.id,
                    Year = albumData.Year
                };
                albumToSave = SaveAlbum(albumToSave);
            }
            
            return artistToSave;
        }

        private Album SaveAlbum(Album albumToSave)
        {
            var album = db.Albums.FirstOrDefault(a => a.Id == albumToSave.Id ||
            a.SpotifyId == albumToSave.SpotifyId);
            if (album == null)
            {
                db.Albums.Add(albumToSave);
                album = albumToSave;
            }
            else
            {
                album.ImageUrl = albumToSave.ImageUrl;
                album.Name = albumToSave.Name;
                album.SpotifyId = albumToSave.SpotifyId;
                album.Popularity = albumToSave.Popularity;
                album.Year = albumToSave.Year;
                album.ArtistId = albumToSave.ArtistId;
            }
            db.SaveChanges();
            return album;
        }

        private Artist SaveArtist(Artist artistToSave)
        {
            var artist = db.Artists.FirstOrDefault(a => a.Id == artistToSave.Id ||
            a.SpotifyId == artistToSave.SpotifyId);
            if (artist == null)
            {
                db.Artists.Add(artistToSave);
                artist = artistToSave;
            }
            else
            {
                artist.ImageUrl = artistToSave.ImageUrl;
                artist.Name = artistToSave.Name;
                artist.SpotifyId = artistToSave.SpotifyId;
            }
            db.SaveChanges();
            return artist;
        }

        public Artist GetArtistById(int? id)
        {
            return id == null ? null : db.Artists.FirstOrDefault(a => a.Id == (int)id);
        }
    }
}
