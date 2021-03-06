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
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        [Key]
        public int Id { get; set; }

        [DisplayName("Numer zamówienia")]
        public string OrderNumber { get; set; }

        [Required]
        [DisplayName("Imię")]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Nazwisko")]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Numer telefonu")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Nazwa karty")]
        public string CardName { get; set; }

        [Required]
        [DisplayName("Numer karty")]
        public string CardNumber { get; set; }

        [Required]
        [DisplayName("Ważność")]
        public string CardValidity { get; set; }

        [Required]
        [DisplayName("CVV")]
        public string CardCvv { get; set; }

        [Required]
        [DisplayName("Ulica")]
        [MaxLength(20)]
        public string Street { get; set; }

        [Required]
        [DisplayName("Miasto")]
        [MaxLength(20)]
        public string City { get; set; }

        [Required]
        [DisplayName("Kraj")]
        [MaxLength(12)]
        public string Country { get; set; }

        [Required]
        [DisplayName("Kod pocztowy")]
        [MaxLength(6)]
        public string ZipCode { get; set; }

        [DisplayName("Data zamówienia")]
        [DisplayFormat(DataFormatString = "{0:f}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayName("Czy zrealizowane")]
        [DefaultValue(false)]
        public bool IsRealized { get; set; }

        [DisplayName("Łączna kwota")]
        [DefaultValue(0.0)]
        [Range(0.1, Double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double TotalAmount { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
