using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG_3A_Part_2_Attempt_3.Models;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    /// <summary>
    /// Controller for account related actions.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="signInManager">The sign-in manager.</param>
        public AccountController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the action result.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage($"/Account/{nameof(Login)}", new { area = "Identity" });
        }

        /// <summary>
        /// Returns the login view.
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
