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
        public List<Artist> GetArtistByName(string name)
        {
            var artist = db.Artists.Where(a => a.Name == name).ToList<Artist>() ?? api.FindArtistByName(name);
            return artist;
        }

        public List<Artist> GetAllDownloadedArtists()
        {
            api.FindArtistByName("");
            return new List<Artist>();
        }

        public Artist GetArtistById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
