using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LibraryDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor,
            LibraryDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password,false,false);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index", "Book");
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
    
            await _httpContextAccessor.HttpContext.SignOutAsync();
            ClearCookies();
            return RedirectToAction("Login", "Account");
        }

        public  void ClearCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Antiforgery.MoniFWH1kX0");
        }
    }
}
