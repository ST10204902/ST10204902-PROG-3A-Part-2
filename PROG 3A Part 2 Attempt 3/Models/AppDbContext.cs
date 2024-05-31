using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the set of Products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the set of FarmerApplications.
        /// </summary>
        public DbSet<FarmerApplication> Farmers { get; set; }

        /// <summary>
        /// Gets or sets the set of AppRoles.
        /// </summary>
        public DbSet<AppRole> AppRoles { get; set; }

        /// <summary>
        /// Gets or sets the set of AppUsers.
        /// </summary>
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
