﻿using EShop.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderAdminRealizedController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderAdminRealizedController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Orders.Where(x => x.IsRealized == true).ToList());
        }

        // GET: Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.FirstOrDefault(c => c.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _db.Orders.Where(c => c.Id == id).FirstOrDefault();

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
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

            var order = _db.Orders.FirstOrDefault(c => c.Id == id);


            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            TempData["delete"] = "Pomyślnie usunięto zamówienie.";

            return RedirectToAction("Index");

        }

        // GET: IsRealized
        public ActionResult IsRealized(int? id)
        {
            var order = _db.Orders.Find(id);
            order.IsRealized = false;

            _db.Orders.Update(order);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
