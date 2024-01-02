using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public AccountController(ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor
            ,UserManager<User> userManager, SignInManager<User> signInManager ,IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
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
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        var curUser = await _userRepository.GetDataForBorrowedBooks(user);
                        foreach( var c in curUser.BorrowedBooks) 
                        {
                            if (CheckingDate(c.DateOfBorrow))
                            {
                                continue;
                            }
                            else
                            {
                                curUser.CanBorrow = false;

                                var update = await _userManager.UpdateAsync(curUser);

                                if (update.Succeeded)
                                {
                                    _userRepository.Update(curUser);
                                    return RedirectToAction("Index");
                                }
                                break;
                            }
                        }
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
            await _signInManager.SignOutAsync();
            ClearCookies();
            return RedirectToAction("Login", "Account");
        }
        
        public bool CheckingDate(DateTime? a) 
        {
            var dateOfBorrow =  a;

            TimeSpan difference = new TimeSpan();
            if (dateOfBorrow != null)
            {
                difference = (TimeSpan)(DateTime.Now - dateOfBorrow);
            }
            return difference.TotalDays < 30;
          
        }
        public  void ClearCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspNetCore.Antiforgery.MoniFWH1kX0");
        }
    }
}
