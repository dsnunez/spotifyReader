﻿using SpotifyMetadata.ResponseModels.Common;
using System.Collections.Generic;

namespace SpotifyMetadata.ResponseModels.Simplified
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#album-object-simplified
    /// </summary>
    public class Album
    {
        public string album_type { get; set; }
        public List<string> available_markets { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
