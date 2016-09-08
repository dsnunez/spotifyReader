using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using SpotifyMetadata.ResponseModels;
using Newtonsoft.Json;

namespace SpotifyMetadata
{
    public class ApiSpotify
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string BaseUrl = "https://api.spotify.com/v1/";

        public ApiSpotify()
        {
            NameValueCollection settings = (NameValueCollection)ConfigurationManager.GetSection("apiKeys");
            ClientId = settings["SpotifyClientId"];
            ClientSecret = settings["SpotifyClientSecret"];
        }

        string ApiGetRequest(string req, bool useBaseUrl = true)
        {
            WebClient client = new WebClient();
            var response = useBaseUrl ? client.DownloadString(BaseUrl + req): client.DownloadString(req);
            return response;
        }

        T GetObjectFromJson<T>(string json)
        {
            if (!String.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }

        internal List<ResponseModels.Full.Artist> SearchArtist(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                //[Spotify Doc]: Encode spaces with the hex code %20 or +.
                query = query.Replace(" ", "+");

                var req = String.Format("search/?q={0}&type=artist", query);
                var response = ApiGetRequest(req);
                var artistJsonResult = GetObjectFromJson<ResponseModels.SearchArtist.SearchArtistJsonResult>(response);
                return artistJsonResult.artists.items;
            }
            else return new List<ResponseModels.Full.Artist>();
        }

        internal ResponseModels.Full.Artist DownloadArtistFullData(string spotifyArtistId)
        {
            var req = String.Format("artists/{0}", spotifyArtistId);
            var response = ApiGetRequest(req);
            var artist = GetObjectFromJson<ResponseModels.Full.Artist>(response);
            return artist;
        }

        internal List<ResponseModels.Simplified.Album> DownloadArtistAlbums(string spotifyArtistId, string albumType = "ep,album")
        {
            var req = String.Format("artists/{0}/albums?album_type={1}", spotifyArtistId, albumType);
            var response = ApiGetRequest(req);
            var page = GetObjectFromJson<ResponseModels.Common.Page<ResponseModels.Simplified.Album>>(response);
            List<ResponseModels.Simplified.Album> albums = new List<ResponseModels.Simplified.Album>();
            albums.AddRange(page.items);
            while(page != null && !String.IsNullOrEmpty(page.next))
            {
                response = ApiGetRequest(page.next, useBaseUrl: false);
                page = GetObjectFromJson<ResponseModels.Common.Page<ResponseModels.Simplified.Album>>(response);
                albums.AddRange(page.items);
            }

            return albums;
        }

        internal ResponseModels.Full.Album DownloadAlbumFullData(string id)
        {
            var req = String.Format("albums/{0}", id);
            var response = ApiGetRequest(req);
            var album = GetObjectFromJson<ResponseModels.Full.Album>(response);
            return album;
        }

        internal List<ResponseModels.Simplified.Track> DownloadAlbumTracks(string spotifyAlbumId)
        {
            var req = String.Format("albums/{0}/tracks", spotifyAlbumId);
            var response = ApiGetRequest(req);
            var page = GetObjectFromJson<ResponseModels.Common.Page<ResponseModels.Simplified.Track>>(response);
            List<ResponseModels.Simplified.Track> tracks = new List<ResponseModels.Simplified.Track>();
            tracks.AddRange(page.items);
            while (page != null && !String.IsNullOrEmpty(page.next))
            {
                response = ApiGetRequest(page.next, useBaseUrl: false);
                page = GetObjectFromJson<ResponseModels.Common.Page<ResponseModels.Simplified.Track>>(response);
                tracks.AddRange(page.items);
            }

            return tracks;
        }

        internal ResponseModels.Full.Track DownloadTrackFullData(string id)
        {
            var req = String.Format("tracks/{0}", id);
            var response = ApiGetRequest(req);
            var track = GetObjectFromJson<ResponseModels.Full.Track>(response);
            return track;
        }
    }
}
