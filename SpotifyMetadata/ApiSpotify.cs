﻿using System;
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

        string ApiGetRequest(string req)
        {
            WebClient client = new WebClient();
            var response = client.DownloadString(BaseUrl + req);
            return response;
        }

        internal List<ResponseModels.Full.Artist> SearchArtist(string query)
        {
            if (!String.IsNullOrWhiteSpace(query))
            {
                //[Spotify Doc]: Encode spaces with the hex code %20 or +.
                query = query.Replace(" ", "+");

                var req = String.Format("search/?q={0}&type=artist", query);
                var response = ApiGetRequest(req);
                var artistJsonResult = JsonConvert.DeserializeObject<ResponseModels.SearchArtist.SearchArtistJsonResult>(response);
                return artistJsonResult.artists.items;
            }
            else return new List<ResponseModels.Full.Artist>();
        }

        internal object DownloadArtist(string spotifyId)
        {
            throw new NotImplementedException();
        }
    }
}
;