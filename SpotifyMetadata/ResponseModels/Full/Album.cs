﻿using SpotifyMetadata.ResponseModels.Common;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyMetadata.ResponseModels.Full
{
    /// <summary>
    /// C# version of https://developer.spotify.com/web-api/object-model/#album-object-full
    /// </summary>
    public class Album
    {
        public string album_type { get; set; }
        public List<Simplified.Artist> artists { get; set; }
        public List<object> available_markets { get; set; }
        public List<Copyright> copyrights { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
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
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public ResponseModels.Common.Page<Simplified.Track> tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public int Year
        {
            get
            {
                int year = 0;
                string[] dateArr = release_date.Split('-');
                if (dateArr.Length > 0)
                    int.TryParse(dateArr[0], out year);
                return year;
            }
        }
    }
}
