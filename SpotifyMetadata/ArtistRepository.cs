using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public ArtistSearchResult SearchArtist(string query, int pageNum = 1, int limit = 5)
        {
            ArtistSearchResult result = new ArtistSearchResult();

            var queryParts = QueryParts(query);

            result.DownloadedMatches = SearchDownloaded(query, pageNum, limit, queryParts);
            result.NotDownloadedMatches = SearchSpotify(query, pageNum, limit, queryParts);

            return result;
        }

        private IEnumerable<string> QueryParts(string query)
        {
            query = System.Web.HttpUtility.UrlDecode(query);
            query = query.ToLower();

            //Separar la query entre palabras sueltas y frases entre comillas, para que se parezca al comportamiento de la API
            return (from Match match in Regex.Matches(query, @"[\""].+?[\""]|[^ ]+")
                    select match.ToString().Trim('"'));
        }

        public Page<Artist> SearchDownloaded(string query, int pageNum, int limit, IEnumerable<string> queryParts = null)
        {
            queryParts = queryParts ?? QueryParts(query);
            var q = SearchDownloadedQuery(queryParts);
            var page = new Page<Artist>(pageNum, limit, q, query);
            return page;
        }

        private IOrderedQueryable<Artist> SearchDownloadedQuery(IEnumerable<string> queryParts)
        {
            return (from a in db.Artists
                    where queryParts.Any(part => a.NameToLower.Contains(part + " ")
                                || a.NameToLower.Contains(" " + part)
                                || a.NameToLower.Equals(part))
                    orderby a.Name
                    select a);
        }

        public Page<Artist> SearchSpotify(string query, int pageNum, int limit, IEnumerable<string> queryParts = null)
        {
            query = query.ToLower();
            var apiMatches = api.SearchArtist(query);

            queryParts = queryParts ?? QueryParts(query);

            var downloadedMatches = SearchDownloadedQuery(queryParts);
            var excludedIDs = new HashSet<string>(downloadedMatches.Select(r => r.SpotifyId));

            var q = (from m in apiMatches
                     where !excludedIDs.Contains(m.id)
                     select new Artist { SpotifyId = m.id, Name = m.name, ImageUrl = m.MainImageUrl })
                     .AsQueryable()
                     .OrderBy(a => a.Name);

            return new Page<Artist>(pageNum, limit, q, query);
        }

        public Page<Artist> GetAllDownloadedArtists(int pageNum, int limit)
        {
            var allArtists = (from a in db.Artists orderby a.Name select a);
            Page<Artist> page = new Page<Artist>(pageNum, limit, allArtists, "");

            return page;
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
                       Img = a.images != null && a.images.Count > 0 && a.images[0] != null ? a.images[0].url : "/images/no-img.png"
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

            track.LastUpdated = DateTime.Now;

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

            album.LastUpdated = DateTime.Now;

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

            artist.LastUpdated= DateTime.Now;

            db.SaveChanges();
            return artist;
        }

        public Artist GetArtistById(int? id)
        {
            return id == null ? null : db.Artists.FirstOrDefault(a => a.Id == (int)id);
        }
    }
}
