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

        string ApiGetRequest(string req)
        {
            WebClient client = new WebClient();
            var response = client.DownloadString(BaseUrl + req);
            return response;
        }

        internal List<Artist> FindArtistByName(string name)
        {
            name = "attalus";
            if (!String.IsNullOrWhiteSpace(name))
            {
                var req = String.Format("search/?q={0}&type=artist", name);
                var response = ApiGetRequest(req);
                var artistResult = JsonConvert.DeserializeObject<ArtistResult>(response);
                return null;
            }
            else return new List<Artist>();
        }
    }
}
;