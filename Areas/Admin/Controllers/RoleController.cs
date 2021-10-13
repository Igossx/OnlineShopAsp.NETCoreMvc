using EShop.Areas.Admin.Models;
using EShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;

        public RoleController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;

            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();

            role.Name = name;

            var isExist = await _roleManager.RoleExistsAsync(role.Name);

            if (isExist)
            {
                ViewBag.mgs = "Taka rola już istnieje!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Pomyślnie zapisano role";
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            role.Name = name;

            var isExist = await _roleManager.RoleExistsAsync(role.Name);

            if (isExist)
            {
                ViewBag.mgs = "Taka rola już istnieje!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["edit"] = "Pomyślnie zaktualizowano rolę";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;

            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["delete"] = "Pomyślnie usunięto rolę";

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Assign()
        {
            var users = _db.ApplicationUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList();
            var roles = _roleManager.Roles.ToList();

            if (users != null && roles != null)
            {
                ViewBag.data = users;
                ViewBag.data2 = roles;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(RoleUserVm roleUser)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == roleUser.UserId);

            var isCheckRoleAssign = await _userManager.IsInRoleAsync(user, roleUser.RoleId);

            if (isCheckRoleAssign)
            {
                ViewBag.mgs = "Ten użytkownik posiada już tę rolę!";

                var users = _db.ApplicationUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList();
                var roles = _roleManager.Roles.ToList();

                if (users != null && roles != null)
                {
                    ViewBag.data = users;
                    ViewBag.data2 = roles;
                }

                //ViewData["UserId"] = new SelectList(_db.ApplicationUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList(), "Id", "UserName");
                //ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");

                return View();
            }

            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);

            if (role.Succeeded)
            {
                TempData["assign"] = "Pomyślnie nadano użytkownikowi rolę";

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult AssignUserRole()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join a in _db.ApplicationUsers on ur.UserId equals a.Id

                         select new UserRoleMaping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName = r.Name
                         };

            ViewBag.UserRoles = result;

            return View();
        }
    }
}
