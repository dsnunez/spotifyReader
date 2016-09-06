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
            throw new NotImplementedException();
        }

        public List<Artist> GetAllDownloadedArtists()
        {
            SearchArtist("\"arrows and\" sound");
            return db.Artists.ToList();
        }

        public Artist DownloadArtistInfo(string spotifyId)
        {
            var artistData = api.DownloadArtistData(spotifyId);
            var albumsData = api.DownloadArtistAlbums(spotifyId);

            var artistToSave = new Artist()
            {
                ImageUrl = artistData.MainImageUrl,
                Name = artistData.name,
                SpotifyId = artistData.id
            };
            SaveArtist(artistToSave);

            return null;
        }

        private void SaveArtist(Artist artistToSave)
        {
            var artist = db.Artists.FirstOrDefault(a => a.Id == artistToSave.Id);
            if (artist == null)
            {
                db.Artists.Add(artistToSave);
            }
            else
            {
                artist.ImageUrl = artistToSave.ImageUrl;
                artist.Name = artistToSave.Name;
                artist.SpotifyId = artistToSave.SpotifyId;
            }
            db.SaveChanges();
        }

        public Artist GetArtistById(int? id)
        {
            return id == null ? null : db.Artists.FirstOrDefault(a => a.Id == (int)id);
        }
    }
}
