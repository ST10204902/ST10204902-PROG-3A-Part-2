using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_3A_Part_2_Attempt_3.Models;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    /// <summary>
    /// Controller for handling user related actions.
    /// </summary>
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="dbContext">The application database context.</param>
        public UsersController(UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        /// <summary>
        /// Returns the create view.
        /// </summary>
        /// <returns>The create view.</returns>
        public IActionResult Create()
        {
            var farmerApplications = _context.Farmers.ToList();
            return View(farmerApplications);
        }

        /// <summary>
        /// Adds a new farmer.
        /// </summary>
        /// <param name="id">The id of the farmer to add.</param>
        /// <returns>The action result.</returns>
        [HttpPost]
        public async Task<IActionResult> AddFarmer(string id)
        {
            var application = await _context.Farmers.FindAsync(id);
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
                    else
                    {
                        // Handle role assignment error
                        ModelState.AddModelError(string.Empty, "Error assigning role to user.");
                        return View(application);
                    }
                }
                else
                {
                    // Handle user creation error
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(application);
                }
            }
            return RedirectToAction("Create");
        }
    }
}
