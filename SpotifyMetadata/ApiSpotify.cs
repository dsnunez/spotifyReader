using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Web;

namespace SpotifyMetadata
{
    public class ApiSpotify
    {
        private string BaseUrl = "https://api.spotify.com/v1/";

        /// <summary>
        /// Calls the API using the endpoint and parameters specified in req. 
        /// </summary>
        /// <param name="req">The endpoint requested and parameters provided. 
        /// It may or may not include the API's base URL</param>
        /// <param name="useBaseUrl">It determines if the base URL should be appended to the req, as 
        /// Spotify's objects sometimes include direct links (that already have the base URL) to other 
        /// API calls</param>
        /// <returns></returns>
        private string ApiGetRequest(string req, bool useBaseUrl = true)
        {
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            try
            {
                var response = useBaseUrl ? client.DownloadString(BaseUrl + req) : client.DownloadString(req);
                return response;
            }
            catch (WebException we)
            {
                return null;
            }
        }

        /// <summary>
        /// Deserializes a JSON string (that may be empty or null) into a T object. If string is null/empty or 
        /// does not correspond to the expected type, the default value for type T is returned
        /// </summary>
        /// <typeparam name="T">Expected type of the json object</typeparam>
        /// <param name="json">JSON string to be deserialized</param>
        /// <returns></returns>
        private T GetObjectFromJson<T>(string json)
        {
            T obj;
            if (!String.IsNullOrWhiteSpace(json))
            {
                try
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                }
                catch (JsonSerializationException jse)
                {
                    obj = default(T);
                }
            }
            else
            {
                obj = default(T);
            }

            return obj;
        }

        /// <summary>
        /// Used to download the complete list of results of type T from Spotify, 
        /// iterating through all of the pages (if Spotify pages the results) 
        /// </summary>
        /// <typeparam name="T">Type of the items requested (from the ResponseModel namespace)</typeparam>
        /// <param name="req">API request that results in a paging object</param>
        /// <returns></returns>
        private List<T> DownloadCompleteListOf<T>(string req)
        {
            var response = ApiGetRequest(req);
            var page = GetObjectFromJson<ResponseModels.Common.Page<T>>(response);
            List<T> list = new List<T>();

            if (page != null && page.items != null)
            {
                list.AddRange(page.items);
                while (page != null && !String.IsNullOrEmpty(page.next))
                {
                    response = ApiGetRequest(page.next, useBaseUrl: false);
                    page = GetObjectFromJson<ResponseModels.Common.Page<T>>(response);
                    if (page != null && page.items != null)
                        list.AddRange(page.items);
                }
            }
            return list;
        }

        private T DownloadFullObject<T>(string req) where T : new()
        {
            var response = ApiGetRequest(req);
            var obj = GetObjectFromJson<T>(response);
            if (obj == null)
                return new T();
            return obj;
        }

        internal List<ResponseModels.Full.Artist> SearchArtist(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                var req = String.Format("search/?q={0}&type=artist", query);
                var response = ApiGetRequest(req);
                var artistJsonResult = GetObjectFromJson<ResponseModels.SearchArtist.SearchArtistJsonResult>(response);
                if (artistJsonResult != null && artistJsonResult.artists != null)
                    return artistJsonResult.artists.items ?? new List<ResponseModels.Full.Artist>();
            }
            return new List<ResponseModels.Full.Artist>();
        }

        internal ResponseModels.Full.Artist DownloadArtistFullData(string spotifyArtistId)
        {
            var req = String.Format("artists/{0}", spotifyArtistId);
            return DownloadFullObject<ResponseModels.Full.Artist>(req);
        }

        internal List<ResponseModels.Simplified.Album> DownloadArtistAlbums(string spotifyArtistId, string albumType = "ep,album", string market = "CL")
        {
            var req = String.Format("artists/{0}/albums?album_type={1}&market={2}", spotifyArtistId, albumType, market);
            return DownloadCompleteListOf<ResponseModels.Simplified.Album>(req);
        }

        internal ResponseModels.Full.Album DownloadAlbumFullData(string id)
        {
            var req = String.Format("albums/{0}", id);
            return DownloadFullObject<ResponseModels.Full.Album>(req); ;
        }

        internal List<ResponseModels.Simplified.Track> DownloadAlbumTracks(string spotifyAlbumId)
        {
            var req = String.Format("albums/{0}/tracks", spotifyAlbumId);
            return DownloadCompleteListOf<ResponseModels.Simplified.Track>(req);
        }

        internal ResponseModels.Full.Track DownloadTrackFullData(string id)
        {
            var req = String.Format("tracks/{0}", id);
            return DownloadFullObject<ResponseModels.Full.Track>(req);
        }
    }
}
