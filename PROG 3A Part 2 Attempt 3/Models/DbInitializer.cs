using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// Provides a method to initialize the database with default data.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initializes the database with some default data.
        /// </summary>
        /// <param name="serviceProvider">The service provider to get required services.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Get the required services
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            // Create the database if it doesn't exist
            await context.Database.EnsureCreatedAsync();

            await context.Database.MigrateAsync();

            string[] roles = { "Farmer", "Employee" };

            // Populate the roles
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole { Name = role });
                }
            }

            // Check if the employee user already exists
            if (await userManager.FindByEmailAsync("employee@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "employee@example.com",
                    Email = "employee@example.com",
                    FirstName = "Administrative",
                    LastName = "Employee",
                    PasswordHash = "Password123!",
                };

                await userManager.CreateAsync(user, "Password123!"); // Set a default password for the user

                await userManager.AddToRoleAsync(user, "Employee"); // Assign the user to the Employee role
            }

            var imageBytes = await File.ReadAllBytesAsync("wwwroot/images/Agri-Energy Connect NoBG.png");

            // Check if the farmer user already exists
            if (await userManager.FindByEmailAsync("farmer1@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "farmer1@example.com",
                    Email = "farmer1@example.com",
                    FirstName = "Farmer",
                    LastName = "One",
                };

                // Set a default password for the user
                var result = await userManager.CreateAsync(user, "Password123!");

                if (result.Succeeded)
                {
                    // Assign the user to the Farmer role
                    await userManager.AddToRoleAsync(user, "Farmer");

                    // Add some products
                    context.Products.AddRange(
                        new Product { Name = "Solar Panel 500W", Category = Category.RenewableEnergy, ProductionDate = DateTime.Now, UserId = user.Id, Photo = imageBytes, Cost = 4000.0 },
                        new Product { Name = "Rainwater Collector 9000", Category = Category.WaterConservation, ProductionDate = DateTime.Now, UserId = user.Id, Photo = imageBytes, Cost = 80000.0 },
                        new Product { Name = "Organic Fertilizer", Category = Category.SoilHealthProducts, ProductionDate = DateTime.Now, UserId = user.Id, Photo = imageBytes, Cost = 100.0 }
                    );

                    await context.SaveChangesAsync();
                }
            }

            // Check if the second farmer user already exists
            if (await userManager.FindByEmailAsync("farmer2@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "farmer2@example.com",
                    Email = "farmer2@example.com",
                    FirstName = "Farmer",
                    LastName = "Two",
                };

                var result = await userManager.CreateAsync(user, "Password123!"); // Set a default password for the user

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Farmer"); // Assign the user to the Farmer role

                    // Add some products
                    context.Products.AddRange(
                        new Product { Name = "Wind Turbine 1KW", Category = Category.RenewableEnergy, ProductionDate = DateTime.Now, UserId = user.Id, Photo = imageBytes, Cost = 4000.0 },
                        new Product { Name = "Drip Irrigation System", Category = Category.WaterConservation, ProductionDate = DateTime.Now, UserId = user.Id, Photo = imageBytes, Cost = 1200.0 },
                        new Product { Name = "Compost Bin", Category = Category.SoilHealthProducts, ProductionDate = DateTime.Now, UserId = user.Id, Photo = imageBytes, Cost = 100.0 }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}