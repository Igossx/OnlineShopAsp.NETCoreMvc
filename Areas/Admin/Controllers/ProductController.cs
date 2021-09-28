using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.Products.ToList();

            return View(data);
        }

        // GET: Details
        public ActionResult Details(int? id)
        {
            var obj = _db.Products.Find(id);

            return View(obj);

        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product products)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(products);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(products);
        }

        // GET: Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product products)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(products);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(products);
        }

        // GET: Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}
