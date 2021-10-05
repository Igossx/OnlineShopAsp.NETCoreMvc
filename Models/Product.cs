using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Nazwa")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Opis")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Grafika")]
        [NotMapped]
        public IFormFile ImageUrl { get; set; }

        [Required]
        [DisplayName("Marka")]
        [MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        [DisplayName("Cena")]
        [Range(0.1, Double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Price { get; set; }

        [Required]
        [DisplayName("Dostępny")]
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        [DisplayName("Kategoria")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [DisplayName("Zdjęcie")]
        public string CoverImageUrl { get; set; }


    }
}
