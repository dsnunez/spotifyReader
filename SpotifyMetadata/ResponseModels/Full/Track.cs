using SpotifyMetadata.ResponseModels.Common;
using System.Collections.Generic;

namespace SpotifyMetadata.ResponseModels.Full
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#track-object-full
    /// </summary>
    public class Track
    {
        public Simplified.Album album { get; set; }
        public List<Simplified.Artist> artists { get; set; }
        public List<object> available_markets { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public object preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
