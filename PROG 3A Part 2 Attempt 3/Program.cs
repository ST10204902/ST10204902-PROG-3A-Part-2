using Microsoft.EntityFrameworkCore;
using PROG_3A_Part_2_Attempt_3.Models;
using Microsoft.AspNetCore.Identity;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices((context, services) =>
        {
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Set the data directory path
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "App_Data"));

            //Configure the DbContext with SQL Server
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

            //Configure Identity
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Identity/Account/Login");
            });
        })
        .Configure((context, app) =>
        {
            //Seed the database
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                DbInitializer.Initialize(services).Wait();
            }

            // Configure the HTTP request pipeline.
            app.UseWhen(x => !x.Request.Path.StartsWithSegments("/health"), appBuilder =>
            {
                if (!context.HostingEnvironment.IsDevelopment())
                {
                    appBuilder.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    appBuilder.UseHsts();
                }

                appBuilder.UseHttpsRedirection();
                appBuilder.UseStaticFiles();

                appBuilder.UseRouting();

                appBuilder.UseAuthentication();
                appBuilder.UseAuthorization();

                appBuilder.UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            });
        });
    });

await builder.RunConsoleAsync();
