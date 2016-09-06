using SpotifyMetadata.ResponseModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyMetadata.ResponseModels.Full
{
    public class Artist
    {
        public ExternalUrls external_urls { get; set; }
        public Followers followers { get; set; }
        public List<object> genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string MainImageUrl
        {
            get
            {
                if (images == null || images.Count() == 0)
                {
                    return "";
                }
                else
                {
                    return images.First().url;
                }
            }
        }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
