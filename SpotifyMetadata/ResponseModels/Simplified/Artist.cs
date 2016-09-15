using SpotifyMetadata.ResponseModels.Common;

namespace SpotifyMetadata.ResponseModels.Simplified
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#artist-object-simplified
    /// </summary>
    public class Artist
    {
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
