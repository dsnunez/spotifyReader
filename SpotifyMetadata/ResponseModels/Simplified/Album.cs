﻿using SpotifyMetadata.ResponseModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyMetadata.ResponseModels.Simplified
{
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
