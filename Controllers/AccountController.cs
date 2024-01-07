using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, SignInManager<User> signInManager ,IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        [ValidateAntiForgeryToken]
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
                        foreach( var borrowedBook in curUser.BorrowedBooks) 
                        {
                            if (CheckingDate(borrowedBook.DateOfBorrow))
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

            TempData["Error"] = "Email does not exist.";
            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        
        public bool CheckingDate(DateTime? dateOfBorrow) 
        {
            TimeSpan difference = new TimeSpan();
            if (dateOfBorrow != null)
            {
                difference = (TimeSpan)(DateTime.Now - dateOfBorrow);
            }
            return difference.TotalDays < 30; 
        }
    }
}
