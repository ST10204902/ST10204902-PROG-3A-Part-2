using Microsoft.AspNetCore.Identity;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    public static class DbInitializer
    {
        /// <summary>
        /// This method initializes the database with some default data
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            //Get the required services
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            //Create the database if it doesn't exist
            context.Database.EnsureCreated();

            string[] roles = { "Farmer", "Employee" };

            //Populate the roles
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole { Name = role });
                }

            }

            //Check if the employee user already exists
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

            //Check if the farmer user already exists
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
                        new Product { Name = "Solar Panel 500W", Category = "Renewable Energy", ProductionDate = DateTime.Now, UserId = user.Id },
                        new Product { Name = "Rainwater Collector 9000", Category = "Water Conservation", ProductionDate = DateTime.Now, UserId = user.Id },
                        new Product { Name = "Organic Fertilizer", Category = "Soil Health Products", ProductionDate = DateTime.Now, UserId = user.Id }
                    );

                    await context.SaveChangesAsync();
                }
            }

            //Check if the second farmer user already exists
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
                        new Product { Name = "Wind Turbine 1KW", Category = "Renewable Energy", ProductionDate = DateTime.Now, UserId = user.Id },
                        new Product { Name = "Drip Irrigation System", Category = "Water Conservation", ProductionDate = DateTime.Now, UserId = user.Id },
                        new Product { Name = "Compost Bin", Category = "Soil Health Products", ProductionDate = DateTime.Now, UserId = user.Id }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
