using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using System.Configuration;
using System.Collections.Specialized;

namespace SpotifyMetadata
{
    public class ApiSpotify
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }

        public ApiSpotify()
        {
            NameValueCollection settings = (NameValueCollection)ConfigurationManager.GetSection("apiKeys");
            ClientId = settings["SpotifyClientId"];
            ClientSecret = settings["SpotifyClientSecret"];
        }

        internal Artist FindArtistByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
;