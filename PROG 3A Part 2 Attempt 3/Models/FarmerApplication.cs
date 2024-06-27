using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// This class represents a farmer application. It inherits from IdentityUser.
    /// Uses a similar constructor to IdentityUser. Used to separate a farmer application from a regular user, 
    /// so that employees can approve the farmer before being added to the DB. Yet, farmers can still apply
    /// </summary>
    public class FarmerApplication : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the farmer.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the farmer.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle names of the farmer.
        /// </summary>
        public string? MiddleNames { get; set; }

        public FarmerApplication(): base()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }
    }
}
