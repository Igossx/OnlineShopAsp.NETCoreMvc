using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Numer zamówienia")]
        public string OrderNumber { get; set; }

        [Required]
        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Adres")]
        [EmailAddress]
        public string Address { get; set; }

        [DisplayName("Data zamówienia")]
        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
