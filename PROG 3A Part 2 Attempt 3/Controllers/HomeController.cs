using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_3A_Part_2_Attempt_3.Models;
using System.Diagnostics;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var allProducts = _context.Products.Include(p => p.User).ToList();
            var userProducts = allProducts.Where(p => p.User?.Email == User.Identity.Name).ToList();
            var otherProducts = allProducts.Except(userProducts).ToList();

            var model = new ProductListsViewModel
            {
                UserProducts = userProducts,
                OtherProducts = otherProducts
            };

            return View(model);
        }


        public async Task<IActionResult> Customers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = role.FirstOrDefault();
            }

            var viewModel = new CustomersViewModel
            {
                Users = users,
                UserRoles = userRoles
            };

            return View(viewModel);
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
