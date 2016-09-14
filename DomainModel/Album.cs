using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DomainModel
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string SpotifyId { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int Popularity { get; set; }

        [NotMapped]
        public double AvgTrackPopularity
        {
            get
            {
                if (Tracks != null && Tracks.Count() > 0)
                {
                    return (from t in Tracks select t.Popularity).Average();
                }
                return 0;
            }
        }

        public string ImageUrl { get; set; }

        [NotMapped]
        public string ImageUrlWithDefault
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ImageUrl) ? ImageUrl : "/images/no-img.png";
            }
        }

        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }

        [InverseProperty("Album")]
        public virtual List<Track> Tracks { get; set; }

        [NotMapped]
        public Track LongestTrack
        {
            get
            {
                if (Tracks != null && Tracks.Count() > 0)
                {
                    return Tracks.Aggregate((currentMax, x) =>
                            (currentMax == null || x.DurationMS > currentMax.DurationMS ? x : currentMax));

                }
                return null;
            }
        }

        public DateTime LastUpdated { get; set; }
    }
}
