using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string SpotifyId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public double Popularity { get; set; }

        public string ImageUrl { get; set; }

        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public virtual Artist Artist{get;set;}

        [InverseProperty("Album")]
        public virtual List<Track> Tracks{get;set;}
    }
}
