using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public UserController(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> UserAdminPanel()
        {
            var users = await _userRepository.GetAll();
            return View(users);
        }
        
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> FilterUsers(string searchedPhrase)
        {
            var filteredBooks = await _userRepository.GetUserByPesel(searchedPhrase);

            if (filteredBooks == null || !filteredBooks.Any())
            {
                return View("UserAdminPanel", new List<User>());
            }

            return View("UserAdminPanel", filteredBooks);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public IActionResult Create()
        {
            var registerVM = new RegisterViewModel();
            return View(registerVM);
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Create(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View("Create", registerVM);

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
                    PostalCode = registerVM.PostalCode,
                    Street = registerVM.Street,
                    City = registerVM.City,
                }
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            else 
            {
                return View("Error");
            }

            return RedirectToAction("UserAdminPanel");
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Edit(string id)
        {
            var user =  await _userRepository.GetById(id);
            if (user == null) return View("Error");

            var userVM = new EditUserViewModel() 
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Pesel = user.Pesel,
                Email = user.Email,
                IdAddress = user.IdAddress,
                City = user.Address.City,
                PostalCode = user.Address.PostalCode,
                Street = user.Address.Street,
            };
            return View(userVM);
        }
        
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Edit(string id, EditUserViewModel userVM) 
        {
            if (!ModelState.IsValid) return View("Edit", userVM);
            
            var emailCheck = await _userManager.FindByEmailAsync(userVM.Email);

            if (emailCheck == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return View("Error");


            if (!string.IsNullOrEmpty(userVM.Password) &&
                !await _userManager.CheckPasswordAsync(user, userVM.Password)) 
            {
                ModelState.AddModelError("Password", "Incorrect Password");
                return View("Edit", userVM);
            }

            if (!string.IsNullOrEmpty(userVM.Password))
            {
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, userVM.Password);
                user.PasswordHash = newPasswordHash;
            }
            
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.Email = userVM.Email;
            user.IdAddress = userVM.IdAddress;
            user.Address = new Address() 
            {
                Id = (int)userVM.IdAddress,
                City = userVM.City,
                PostalCode = userVM.PostalCode,
                Street = userVM.Street
            };
     
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _userRepository.Update(user);
                return RedirectToAction("UserAdminPanel");
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Failed to update user");
                return View("Edit", userVM);
            }
        }
       
        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> DeleteUser(string id) 
        {
            var user = await _userRepository.GetByIdAsyncNoTracking(id);
            if (user == null) return View("Error");
            _userRepository.Delete(user);
            return RedirectToAction("UserAdminPanel");
        }
    }
}
