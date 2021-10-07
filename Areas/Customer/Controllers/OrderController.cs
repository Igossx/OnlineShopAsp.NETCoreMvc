using EShop.Data;
using EShop.Models;
using EShop.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Checkout(Order anOrder)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    anOrder.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNumber = GetOrderNumber();
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
