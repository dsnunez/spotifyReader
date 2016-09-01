using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyMetadata
{
    public class ArtistRepository
    {
        SpotifyContext db = new SpotifyContext();
        ApiSpotify api = new ApiSpotify();
        public Artist GetArtistByName(string name)
        {
            var artist = db.Artists.FirstOrDefault(a => a.Name == name) ?? api.FindArtistByName(name);
            return artist;
        }

        public List<Artist> GetAllDownloadedArtists()
        {
            throw new NotImplementedException();
        }

        public Artist GetArtistById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
