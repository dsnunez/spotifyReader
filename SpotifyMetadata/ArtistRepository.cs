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
        public ArtistSearchResult SearchArtistByName(string name)
        {
            ArtistSearchResult result = new ArtistSearchResult();
            result.DownloadedMatches = db.Artists.Where(a => a.Name == name).ToList<Artist>();

            var apiMatches = api.FindArtistByName(name);
            var excludedIDs = new HashSet<string>(result.DownloadedMatches.Select(r => r.SpotifyId));
            result.NotDownloadedMatches = (from m in apiMatches
                                          where !excludedIDs.Contains(m.id)
                                          select new Artist { SpotifyId = m.id, Name = m.name })
                                          .ToList();

            return result;
        }

        public List<Artist> GetAllDownloadedArtists()
        {
            SearchArtistByName("attalus");
            return db.Artists.ToList();
        }

        public Artist GetArtistById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
