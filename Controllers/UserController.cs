using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> UserAdminPanel() 
        {
            return View();  
        }

        [HttpGet]
        public IActionResult Register()
        {
            var registerVM = new RegisterViewModel();
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid) return View("Error");

            var emailCheck = await _userManager.FindByEmailAsync(registerVM.Email);

            if (emailCheck != null)
            {
                return View("Error");
            }

            var newUser = new User()
            {
                UserName = registerVM.Email,
                Email = registerVM.Email,
                EmailConfirmed = true,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Pesel = registerVM.Pesel,
                Address = new Address()
                {
                    PostalCode = registerVM.Address.PostalCode,
                    Street = registerVM.Address.Street,
                    City = registerVM.Address.City,
                }
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return View("Index");
        }
    }
}
