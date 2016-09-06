using SpotifyMetadata.ResponseModels.Common;
using SpotifyMetadata.ResponseModels.Full;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyMetadata.ResponseModels.SearchArtist
{
    public class SearchArtistJsonResult
    {
        public Page<Artist> artists { get; set; }
    }
    
    //
    // Clases generadas con http://json2csharp.com/
    // Usando el string del artista de ejemplo cmo base para el código
    //

}
