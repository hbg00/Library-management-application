using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly UserManager<User> _userManager;

        public BookController(IBookRepository bookRepository, IUserRepository userRepository, UserManager<User> userManager, IBorrowedBookRepository borrowedBookRepository)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _borrowedBookRepository = borrowedBookRepository;
        }

        [HttpGet]
        [Authorize(Roles = "librarian,user")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();

            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "librarian,user")]
        public async Task<IActionResult> DetailBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            return book == null ? NotFound() : View(book);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> BooksAdminPanel()
        {
            var books = await _bookRepository.GetAll();
            return View(books);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public IActionResult Create()
        {
            var bookVM = new CreateBookViewModel();
            return View(bookVM);
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public IActionResult Create(CreateBookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    ISBN = bookVM.ISBN,
                    Title = bookVM.Title,
                    Description = bookVM.Description,
                    NumberOfCopies = bookVM.NumberOfCopies,
                    Language = bookVM.Language,

                    Publisher = new Publisher
                    {
                        FirstName = bookVM.Publisher.FirstName,
                        LastName = bookVM.Publisher.LastName,
                        Biography = bookVM.Publisher.Biography,
                        DateOfBirth = bookVM.DateOfBirth
                    }
                };

                _bookRepository.Add(book);
                return RedirectToAction("BooksAdminPanel");
            }
            return View(bookVM);
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null) return View("Error");

            var bookVM = new EditBookViewModel()
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                NumberOfCopies = book.NumberOfCopies,
                Language = book.Language,
                IdPublisher = book.IdPublisher,
            };

            return View(bookVM);
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Edit(int id, EditBookViewModel editVM)
        {
            if (!ModelState.IsValid) return View("Edit", editVM);

            var bookDb = await _bookRepository.GetByIdAsyncNoTracking(id);

            if (bookDb == null) return View("Error");

            if (editVM.IdPublisher.HasValue)
            {
                var book = new Book
                {
                    Id = id,
                    ISBN = editVM.ISBN,
                    Title = editVM.Title,
                    Description = editVM.Description,
                    NumberOfCopies = editVM.NumberOfCopies,
                    Language = editVM.Language,
                    IdPublisher = editVM.IdPublisher,
                };

                _bookRepository.Update(book);
                return RedirectToAction("BooksAdminPanel");
            }
            else
            {
                ModelState.AddModelError("Publisher", "Publisher is required.");
                return View("Edit", editVM);
            }
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null) return View("Error");

            return View(book);
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null) return View("Error");

            _bookRepository.Delete(book);
            return RedirectToAction("BooksAdminPanel");
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var book = await _bookRepository.GetByIdAsyncForNumberOfCopies(id);

            return book == null ? NotFound() : View(book);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> BorrowBookConfirm(int id, int numberOfWantedCopies)
        {
            if (numberOfWantedCopies < 0) return NotFound();

            var book = await _bookRepository.GetByIdAsyncNoTracking(id);
            var curUser = await _userManager.GetUserAsync(User);

            if (curUser == null || book == null) return NotFound();

            var substruct = book.NumberOfCopies - numberOfWantedCopies;
            var boolcheck = book.NumberOfCopies >= numberOfWantedCopies;

            if (curUser.CanBorrow)
            {
                if (boolcheck && substruct >= 0)
                {
                    book.NumberOfCopies = substruct;

                    var borrowedBook = new BorrowedBook
                    {
                        NumberOfBorrowedCopies = numberOfWantedCopies,
                        DateOfBorrow = DateTime.Now,
                        IdBook = id,
                        UserId = curUser.Id,
                    };
                    _borrowedBookRepository.Add(borrowedBook);
                    _bookRepository.Update(book);

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> ReturnBook(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return View("Error");

            var borrowedBooks = await _userRepository.GetDataForBorrowedBooks(user);

            return View(borrowedBooks);
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var borrowedBook = await _borrowedBookRepository.GetByIdAsyncNoTracking(id);
            var userId = borrowedBook.UserId;
            if (borrowedBook == null) return NotFound();

            var book = await _bookRepository.GetByIdAsyncNoTracking((int)borrowedBook.IdBook);

            if (book == null) return NotFound();

            
            book.NumberOfCopies += borrowedBook.NumberOfBorrowedCopies;
            _borrowedBookRepository.Delete(borrowedBook);
            _bookRepository.Update(book);

            return RedirectToAction("ReturnBook", new { id = userId });
        }

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> AllowBorrowBook()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return View("Error");

            user.CanBorrow = true;

            var update = await _userManager.UpdateAsync(user);
            if (update.Succeeded)
            {
                _userRepository.Update(user);
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        [HttpGet]
        [Authorize(Roles = "librarian,user")]
        public async Task<IActionResult> FilterBooksUser(string searchedPhrase)
        {
            var filteredBooks = await _bookRepository.GetBooksByName(searchedPhrase);

            if (filteredBooks == null || !filteredBooks.Any())
            {
                return View("Index", new List<Book>());
            }

            return View("Index", filteredBooks);
        }

        [HttpGet]
        [Authorize(Roles = "librarian,user")]
        public async Task<IActionResult> FilterBooksAdmin(string searchedPhrase)
        {
            var filteredBooks = await _bookRepository.GetBooksByName(searchedPhrase);

            if (filteredBooks == null || !filteredBooks.Any())
            {
                return View("BooksAdminPanel", new List<Book>());
            }

            return View("BooksAdminPanel", filteredBooks);
        }
    }
}
