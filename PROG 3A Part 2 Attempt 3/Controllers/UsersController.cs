using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_3A_Part_2_Attempt_3.Models;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        public IActionResult Create()
        {
            var farmerApplications = _context.Farmers.ToList();
            return View(farmerApplications);
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(string id)
        {
            var application = _context.Farmers.Find(id);
            if (application != null)
            {
                var user = new AppUser
                {
                    UserName = application.UserName,
                    Email = application.Email,
                    FirstName = application.FirstName,
                    LastName = application.LastName,
                    MiddleNames = application.MiddleNames,
                    PasswordHash = application.PasswordHash,
                    NormalizedEmail = application.NormalizedEmail,
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    // Add the user to the "Farmer" role
                    var roleResult = await _userManager.AddToRoleAsync(user, "Farmer");
                    if (roleResult.Succeeded)
                    {
                        _context.Farmers.Remove(application);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("Create");
        }
    }
}
