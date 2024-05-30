using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG_3A_Part_2_Attempt_3.Models;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
