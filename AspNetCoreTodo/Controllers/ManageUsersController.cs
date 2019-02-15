using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreTodo.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ManageUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /<controller>/

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var roles =  await _userManager.GetRolesAsync(currentUser);
            if(!roles.Contains(Constants.AdministratorRole))
            {
                return Redirect("/Identity/Account/AccessDenied?ReturnUrl=%2FManageUsers");
            }
            var admins = (await _userManager
                .GetUsersInRoleAsync("Administrator"))
                .ToArray();

            var everyone = _userManager.Users.ToArray();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone

            };

            return View(model);
        }
    }
}
