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

        public void DeleteAll()
        {
            db.Tracks.RemoveRange(db.Tracks);
            db.Albums.RemoveRange(db.Albums);
            db.Artists.RemoveRange(db.Artists);
            db.SaveChanges();
        }

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
                var wordFollowed = part + " ";
                var wordLast = " " + part;
                result.DownloadedMatches.AddRange(db.Artists
                    .Where(a => a.NameToLower.Contains(wordFollowed) 
                                || a.NameToLower.Contains(wordLast)
                                || a.NameToLower.Equals(part))); 
            }
            result.DownloadedMatches = result.DownloadedMatches.Distinct().ToList();
            var apiMatches = api.SearchArtist(query);
            var excludedIDs = new HashSet<string>(result.DownloadedMatches.Select(r => r.SpotifyId));
            result.NotDownloadedMatches = (from m in apiMatches
                                           where !excludedIDs.Contains(m.id)
                                           select new Artist { SpotifyId = m.id, Name = m.name, ImageUrl = m.MainImageUrl })
                                          .ToList();

            return result;
        }

        public List<Artist> GetAllDownloadedArtists()
        {
            return db.Artists.ToList();
        }

        public int DownloadArtistBasicInfo(string spotifyId)
        {
            var artistData = api.DownloadArtistFullData(spotifyId);
            var artistToSave = new Artist()
            {
                ImageUrl = artistData.MainImageUrl,
                Name = artistData.name,
                NameToLower = artistData.name.ToLower(),
                SpotifyId = artistData.id
            };
            artistToSave = SaveArtist(artistToSave);
            return artistToSave.Id;
        }

        public IEnumerable<dynamic> DownloadArtistAlbumList(string spotifyId)
        {
            var albumsList = api.DownloadArtistAlbums(spotifyId);
            return from a in albumsList
                   select new
                   {
                       Key = a.id,
                       Value = a.name,
                       Img = a.images != null && a.images.Count > 0 && a.images[0] != null ? a.images[0].url : ""
                   };
        }

        public int DownloadAlbumBasicInfo(string spotifyId, int artistId)
        {
            var albumData = api.DownloadAlbumFullData(spotifyId);
            var albumToSave = new Album()
            {
                ArtistId = artistId,
                ImageUrl = albumData.MainImageUrl,
                Name = albumData.name,
                Popularity = albumData.popularity,
                SpotifyId = albumData.id,
                Year = albumData.Year
            };
            albumToSave = SaveAlbum(albumToSave);
            return albumToSave.Id;
        }

        public IEnumerable<KeyValuePair<string, string>> DownloadAlbumTrackList(string spotifyId)
        {
            var albumTracksList = api.DownloadAlbumTracks(spotifyId);
            return from t in albumTracksList
                   select new KeyValuePair<string, string>(t.id, t.name);
        }

        public void DownloadTrack(string spotifyId, int albumId)
        {
            var trackData = api.DownloadTrackFullData(spotifyId);
            var trackToSave = new Track()
            {
                AlbumId = albumId,
                DurationMS = trackData.duration_ms,
                Name = trackData.name,
                SpotifyId = trackData.id,
                Popularity = trackData.popularity,
                TrackNumber = trackData.track_number,
                DiscNumber = trackData.disc_number
            };
            trackToSave = SaveTrack(trackToSave);
        }

        private Track SaveTrack(Track trackToSave)
        {
            var track = db.Tracks.FirstOrDefault(a => a.Id == trackToSave.Id ||
            a.SpotifyId == trackToSave.SpotifyId);
            if (track == null)
            {
                db.Tracks.Add(trackToSave);
                track = trackToSave;
            }
            else
            {
                track.Name = trackToSave.Name;
                track.SpotifyId = trackToSave.SpotifyId;
                track.Popularity = trackToSave.Popularity;
                track.AlbumId = trackToSave.AlbumId;
                track.DiscNumber = trackToSave.DiscNumber;
                track.DurationMS = trackToSave.DurationMS;
                track.TrackNumber = trackToSave.TrackNumber;
            }
            db.SaveChanges();
            return track;
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
                artist.NameToLower = artistToSave.Name.ToLower();
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
