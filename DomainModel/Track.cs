using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        public string SpotifyId { get; set; }

        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }

        public int TrackNumber { get; set; }
        public string Name { get; set; }
        public double Popularity { get; set; }
        public int DurationMS { get; set; }

        [NotMapped]
        public int DurationMinutes
        {
            get
            {
                return (int)TimeSpan.FromMilliseconds(DurationMS).TotalMinutes;
            }
        }

        [NotMapped]
        public int DurationSeconds
        {
            get
            {
                return (int)TimeSpan.FromMilliseconds(DurationMS).TotalSeconds;
            }
        }

    }
}
