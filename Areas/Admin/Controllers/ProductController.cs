using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _whe;

        [Obsolete]
        public ProductController(ApplicationDbContext db, IWebHostEnvironment whe)
        {
            _db = db;
            _whe = whe;
        }

        public IActionResult Index()
        {
            return View(_db.Products.Include(x => x.Category).ToList());
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

        // GET: Create
        public IActionResult Create()
        {
            var items = _db.Categories.ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product products)
        {

            if (ModelState.IsValid)
            {
                if (products.ImageUrl != null)
                {
                    string folder = "products/image/";
                    folder += products.ImageUrl.FileName + Guid.NewGuid().ToString();
                    string serverFolder = Path.Combine(_whe.WebRootPath, folder);
                    await products.ImageUrl.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                _db.Products.Add(products);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var items = _db.Categories.ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }

            return View(products);
        }

        // GET: Edit
        public IActionResult Edit(int? id)
        {

            var items = _db.Categories.ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }

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

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product products)
        {
            if (ModelState.IsValid)
            {
                if (products.ImageUrl != null)
                {
                    string folder = "products/image/";
                    folder += products.ImageUrl.FileName + Guid.NewGuid().ToString();
                    string serverFolder = Path.Combine(_whe.WebRootPath, folder);
                    await products.ImageUrl.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                _db.Products.Update(products);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var items = _db.Categories.ToList();
            if (items != null)
            {
                ViewBag.data = items;
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

            var product = _db.Products.Include(c => c.Category).Where(c => c.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            var items = _db.Categories.ToList();
            if (items != null)
            {
                ViewBag.data = items;
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

            var product = _db.Products.FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var items = _db.Categories.ToList();
            if (items != null)
            {
                ViewBag.data = items;
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}
