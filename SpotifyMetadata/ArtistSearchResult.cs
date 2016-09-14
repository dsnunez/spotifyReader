using System.Collections.Generic;
using DomainModel;

namespace SpotifyMetadata
{
    public class ArtistSearchResult
    {
        public string Query { get; set; }
        public List<Artist> DownloadedMatches { get; internal set; }
        public List<Artist> NotDownloadedMatches { get; internal set; }

        public ArtistSearchResult()
        {
            DownloadedMatches = new List<Artist>();
        }
    }
}