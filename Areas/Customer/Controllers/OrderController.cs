using EShop.Data;
using EShop.Models;
using EShop.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Checkout

        public IActionResult Checkout()
        {
            return View();
        }

        // POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutEnd(Order anOrder)
        {
            var totalAmount = 0.0;
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    totalAmount += product.Price;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNumber = GetOrderNumber();
            anOrder.OrderDate = DateTime.Now;
            anOrder.TotalAmount = totalAmount;

            // Sending email

            //string to = anOrder.Email; // To address    
            //string from = "Eshop@gmail.com"; // From address    
            //MailMessage message = new MailMessage(from, to);

            //message.Subject = "Podsumowanie twojego zamówienia nr " + anOrder.OrderNumber + ".";

            //string mailbody = "Szczegóły twojego zamówienia: " + Environment.NewLine + "Telefon: " + anOrder.PhoneNumber + Environment.NewLine +
            //    "Adres: " + anOrder.Country + " " + anOrder.ZipCode + " " + anOrder.City + " " + anOrder.Street + Environment.NewLine +
            //    "Wartość całkowita zamówienia: " + anOrder.TotalAmount + " zł.";

            //message.Body = mailbody;
            //message.BodyEncoding = Encoding.UTF8;
            //message.IsBodyHtml = true;
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential("emailId", "Password");
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;
            //try
            //{
            //    client.Send(message);
            //}

            //catch (Exception ex)
            //{
            //    throw;
            //}

            _db.Orders.Add(anOrder);

            await _db.SaveChangesAsync();

            HttpContext.Session.Set("products", new List<Product>());

            return View();
        }

        public string GetOrderNumber()
        {
            int rowCount = _db.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
