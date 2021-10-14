using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(ApplicationUser user)
        {

            if (ModelState.IsValid)
            {
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded)
                {
                    var isSaveRole = await _userManager.AddToRoleAsync(user, "Użytkownik");

                    TempData["save"] = "Pomyślnie utworzono użytkownika.";
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);

            if (userInfo == null)
            {
                return NotFound();
            }

            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;

            var result = await _userManager.UpdateAsync(userInfo);

            if (result.Succeeded)
            {
                TempData["edit"] = "Pomyślnie edytowano użytkownika.";
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);

            if (userInfo == null)
            {
                return NotFound();

            }
            _db.ApplicationUsers.Remove(userInfo);

            int rowAffected = _db.SaveChanges();

            if (rowAffected > 0)
            {
                TempData["delete"] = "Pomyślnie usunięto użytkownika.";
                return RedirectToAction("Index");
            }
            return View(userInfo);
        }

        public async Task<IActionResult> Locout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Locout(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);

            if (userInfo == null)
            {
                return NotFound();

            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(30);

            int rowAffected = _db.SaveChanges();

            if (rowAffected > 0)
            {
                TempData["lockout"] = "Pomyślnie zablokowano użytkownika.";
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }

        public async Task<IActionResult> Active(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser user)
        {
            var userInfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);

            if (userInfo == null)
            {
                return NotFound();

            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);

            int rowAffected = _db.SaveChanges();

            if (rowAffected > 0)
            {
                TempData["active"] = "Pomyślnie odblokowano użytkownika.";
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }

    }
}
