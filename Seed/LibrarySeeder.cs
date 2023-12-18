using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Seed
{
    public class LibrarySeeder
    {
        public static void SeedDatat(IApplicationBuilder applicationBuilder) 
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();
                context.Database.EnsureCreated();
                
            }
        }

        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder) 
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                //Checking for roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Librarian))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Librarian));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                //Adding admin account
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminEmail = "emamiler@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null) 
                {
                    var admin = new User()
                    {
                        UserName = "emamiler",
                        Email = adminEmail,
                        EmailConfirmed = true,
                        FirstName = "Ema",
                        LastName = "Miler",

                    };
                    await userManager.CreateAsync(admin, "Test1234$");
                    await userManager.AddToRoleAsync(admin, UserRoles.Librarian);
                }

                //Adding user account
                string userEmail = "testuser@gmail.com";

                var user = await userManager.FindByEmailAsync(userEmail);
                
                if (user == null) 
                {
                    var newUser = new User()
                    {
                        UserName = "app-user",
                        Email = userEmail,
                        EmailConfirmed = true,
                        FirstName = "test",
                        LastName = "test",
                    };
                    await userManager.CreateAsync(newUser, "Test1234$");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
