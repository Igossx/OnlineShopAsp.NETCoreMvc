using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nazwa")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Opis")]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Model")]
        [MaxLength(25)]
        public string Model { get; set; }

        [Required]
        [DisplayName("Marka")]
        [MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        [DisplayName("Cena")]
        [Range(0.1, Double.MaxValue)]
        public double Price { get; set; }
    }
}
