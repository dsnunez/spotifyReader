using SpotifyMetadata.ResponseModels.Common;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyMetadata.ResponseModels.Full
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#artist-object-full
    /// </summary>
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
