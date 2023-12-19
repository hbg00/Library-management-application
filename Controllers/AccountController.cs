using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly LibraryDbContext _context;
        public AccountController(LibraryDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var loginVM = new LoginViewModel();

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM) 
        {
            if(!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null) 
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck) 
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                TempData["Error"] = "Password is invalid.";
                return View(loginVM);   
            }
            TempData["Error"] = "Email is invalid.";
            return View(loginVM);
        }
        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public void RegisterUser()
        {


        }
    }
}
