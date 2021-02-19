using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Diplom.Data;
using Microsoft.AspNetCore.Identity;
using Diplom.ViewModels;

namespace Diplom.Controllers
{
    public class RoleManagmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagmentController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Rights
        public async Task<IActionResult> Index()
        {
            List<RoleManagmentViewModel> UserRoleList = new List<RoleManagmentViewModel>();
            foreach (IdentityRole role in _roleManager.Roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);

                foreach (User identityUser in users)
                {
                    UserRoleList.Add(new RoleManagmentViewModel()
                    {
                        UserName = identityUser.UserName,
                        ApplicationRoleName = role.Name
                    });
                }
            }

            return View(UserRoleList);
        }

        public async Task<IActionResult> GiveOutRole()
        {
            ViewData["UserName"] = new SelectList(_userManager.Users, "UserName", "UserName");
            ViewData["RoleName"] = new SelectList(_roleManager.Roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GiveOutRole([Bind("UserName,ApplicationRoleName")] RoleManagmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                await _userManager.AddToRoleAsync(user, model.ApplicationRoleName);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = new SelectList(_userManager.Users, "UserName", "UserName");
            ViewData["RoleName"] = new SelectList(_roleManager.Roles, "Name", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string UserName, string ApplicationRoleName)
        {

            var user = await _userManager.FindByNameAsync(UserName);
            await _userManager.RemoveFromRoleAsync(user, ApplicationRoleName);
            return RedirectToAction(nameof(Index));

        }

    }
}
