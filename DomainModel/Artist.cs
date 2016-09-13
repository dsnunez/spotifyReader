using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameToLower { get; set; } //Cachear este valor, para agilizar la búsqueda
        public string SpotifyId { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public string ImageUrlWithDefault
        {
            get
            {
                return !String.IsNullOrWhiteSpace(ImageUrl) ? ImageUrl : "/images/no-img.png";
            }
        }

        [InverseProperty("Artist")]
        public virtual List<Album> Albums { get; set; }
    }
}
