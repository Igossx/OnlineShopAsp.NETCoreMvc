using EShop.Data;
using EShop.Models;
using EShop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(x => x.Category).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // POST: Details
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(int? id)
        {
            List<Product> products = new List<Product>();

            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Product>>("products");

            if (products == null)
            {
                products = new List<Product>();
            }

            products.Add(product);
            HttpContext.Session.Set("products", products);

            return RedirectToAction("Index");
        }

        // POST: Remove
        [HttpPost]
        public ActionResult Remove(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(x => x.Id == id);
                if (products != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Cart
        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products == null)
            {
                products = new List<Product>();
            }

            return View(products);
        }

        // GET: Remove
        [ActionName("Remove")]
        public ActionResult RemoveToCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(x => x.Id == id);
                if (products != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
