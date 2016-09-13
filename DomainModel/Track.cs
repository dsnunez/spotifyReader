using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int DiscNumber { get; set; }
        public int TrackNumber { get; set; }
        public string Name { get; set; }
        public double Popularity { get; set; }
        public int DurationMS { get; set; }

        [NotMapped]
        public string DurationStringMMSS
        {
            get
            {
                int minutes = (int)TimeSpan.FromMilliseconds(DurationMS).TotalMinutes;
                int totalSeconds = (int)TimeSpan.FromMilliseconds(DurationMS).TotalSeconds;
                int seconds = totalSeconds - minutes * 60;

                return String.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
