using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibraryDbContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDbConnection")));
            
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LibraryDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie();


            var app = builder.Build();

            // Seeding 
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
             
               //LibrarySeeder.SeedData(app);
               //LibrarySeeder.SeedUsersAndRoles(app);
            }


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Account/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}