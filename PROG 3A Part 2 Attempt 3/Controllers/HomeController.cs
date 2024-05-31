using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_3A_Part_2_Attempt_3.Models;
using System.Diagnostics;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    /// <summary>
    /// Controller for handling home related actions.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="context">The application database context.</param>
        /// <param name="userManager">The user manager.</param>
        public HomeController(ILogger<HomeController> logger, AppDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns the home page view.
        /// </summary>
        /// <returns>The home page view.</returns>
        public async Task<IActionResult> Index()
        {
            var allProducts = await _context.Products.Include(p => p.User).ToListAsync();
            var userProducts = allProducts.Where(p => p.User?.Email == User.Identity.Name).ToList();
            var otherProducts = allProducts.Except(userProducts).ToList();

            var model = new ProductListsViewModel
            {
                UserProducts = userProducts,
                OtherProducts = otherProducts
            };

            return View(model);
        }

        /// <summary>
        /// Returns the customers view.
        /// </summary>
        /// <returns>The customers view.</returns>
        public async Task<IActionResult> Customers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = role.Any() ? role.First() : null;
            }

            var viewModel = new CustomersViewModel
            {
                Users = users,
                UserRoles = userRoles
            };

            return View(viewModel);
        }

        /// <summary>
        /// Returns the error view.
        /// </summary>
        /// <returns>The error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// This method filters the products based on the provided criteria.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<IActionResult> Filter(DateTime? startDate, DateTime? endDate, string category)
        {
            var allProducts = await _context.Products.Include(p => p.User).ToListAsync();

            if (startDate.HasValue)
            {
                allProducts = allProducts.Where(p => p.ProductionDate >= startDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                allProducts = allProducts.Where(p => p.ProductionDate <= endDate.Value).ToList();
            }

            if (!string.IsNullOrEmpty(category) && Enum.TryParse(category, out Category categoryEnum))
            {
                allProducts = allProducts.Where(p => p.Category == categoryEnum).ToList();
            }

            var userId = _userManager.GetUserId(User);
            var userProducts = allProducts.Where(p => p.UserId == userId).ToList();
            var otherProducts = allProducts.Where(p => p.UserId != userId).ToList();

            var model = new ProductListsViewModel
            {
                UserProducts = userProducts,
                OtherProducts = otherProducts
            };

            return View("Index", model);
        }
    }
}
