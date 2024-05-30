using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <inheritdoc />
    
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle names of the user.
        /// </summary>
        public string? MiddleNames { get; set; }


        /// <summary>
        /// Gets or sets the products of the user.
        /// </summary>
        public ICollection<Product>? Products { get; set; }
    }
}
