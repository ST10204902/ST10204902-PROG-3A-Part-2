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
    public class ApplyFarmer : PageModel
    {
        private readonly ILogger<FarmerApplication> _logger;
        private readonly AppDbContext _context;
        

        /// <summary>
        /// Constructor for ApplyFarmer.
        /// </summary>
        public ApplyFarmer(ILogger<FarmerApplication> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        /// <summary>
        /// Input model for the Register page.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

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
        /// Handles the POST request for the Register page.
        /// </summary>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var existingFarmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Email == Input.Email);
                
                if (existingFarmer != null)
                {
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

                await _context.Farmers.AddAsync(farmer);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User created a new account with password.");
                TempData["SuccessMessage"] = "You have successfully applied";
            }

            return Page();
        }
    }
}