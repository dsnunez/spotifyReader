using System.Collections.Generic;

namespace SpotifyMetadata.ResponseModels.Common
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#paging-object
    /// </summary>
    /// <typeparam name="T">Type of the requested data</typeparam>
    public class Page<T>
    {
        public string href { get; set; }
        public List<T> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public string previous { get; set; }
        public int total { get; set; }
    }
}
