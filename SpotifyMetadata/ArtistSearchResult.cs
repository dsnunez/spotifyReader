using System.Collections.Generic;
using DomainModel;

namespace SpotifyMetadata
{
    public class ArtistSearchResult
    { 
        public Page<Artist> DownloadedMatches { get; internal set; }
        public Page<Artist> NotDownloadedMatches { get; internal set; }
    }
}