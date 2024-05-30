// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PROG_3A_Part_2_Attempt_3.Models;
using SQLitePCL;

namespace PROG_3A_Part_2_Attempt_3.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly AppDbContext _context;

        public RegisterModel(ILogger<RegisterModel> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Middle Names")]
            public string MiddleNames { get; set; }
        }

        /// <summary>
        /// This method is called when the user clicks the register button.
        /// It validates the input and creates a new user.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //Check if a user with the same email already exists
                var existingUser = await _context.Farmers.FirstOrDefaultAsync(f => f.Email == Input.Email);

                if (existingUser != null)
                {
                    // Add model error and return the page to display error
                    TempData["ErrorMessage"] = "A user with the same email already exists.";
                    return Page();
                }

                var farmer = new FarmerApplication
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = Input.Email,
                    NormalizedUserName = Input.Email.ToUpper(),
                    Email = Input.Email,
                    NormalizedEmail = Input.Email.ToUpper(),
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    MiddleNames = Input.MiddleNames,
                };

                var passwordHasher = new PasswordHasher<FarmerApplication>();
                farmer.PasswordHash = passwordHasher.HashPassword(farmer, Input.Password);

                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User created a new account with password.");
                TempData["SuccessMessage"] = "You have successfully applied";
                //return RedirectToPage("/RegistrationConfirmation", new { email = Input.Email });
            }

            return Page();
        }

        /// <summary>
        /// Creates a new instance of <see cref="FarmerApplication"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private FarmerApplication CreateFarmerApplication()
        {
            try
            {
                return Activator.CreateInstance<FarmerApplication>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(FarmerApplication)}'. " +
                    $"Ensure that '{nameof(FarmerApplication)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/ApplyFarmer.cshtml");
            }
        }
    }
}