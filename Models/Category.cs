using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Kategoria")]
        [MaxLength(15)]
        public string CategoryName { get; set; }

    }
}
