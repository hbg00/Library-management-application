using BookStore.Interfaces;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> DetailBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            return book == null ? NotFound() : View(book);
        }

        [HttpGet]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var book = await _bookRepository.GetByIdAsyncForNumberOfCopies(id);

            return book == null ? NotFound() : View(book);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBookConfirm(int id)
        {
            var book = await _bookRepository.GetByIdAsyncForNumberOfCopies(id);
            
            if (book == null) return NotFound();

            book.NumberOfCopies -= 1;
            _bookRepository.Update(book);
            return RedirectToAction("Index");
            //Pop up with u borrowed book 
        }

        [HttpGet]
        public async Task<IActionResult> BooksAdminPanel() 
        {
            var books = await _bookRepository.GetAll(); 
            return View(books);
        }

        [HttpGet]
        public  IActionResult Create()
        {
            var bookVM = new CreateBookViewModel();
            return View(bookVM);
        }

        [HttpPost]
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
                        DateOfBrith = bookVM.DateOfBirth 
                    }
                };

                _bookRepository.Add(book);
                return RedirectToAction("BooksAdminPanel");
            }
            return View(bookVM);
        }

        [HttpGet]
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
                Publisher = book.Publisher
            };

            return View(bookVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookViewModel editVM)
        {
            if (!ModelState.IsValid) return View("Edit", editVM);

            var bookDb = await _bookRepository.GetByIdAsyncNoTracking(id);

            if (bookDb == null) return View("Error");

            // Sprawdź, czy IdPublisher i Publisher są ustawione
            if (editVM.IdPublisher.HasValue && editVM.Publisher != null)
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
                    Publisher = new Publisher {
                        Id = (int)editVM.IdPublisher,
                        FirstName = editVM.Publisher.FirstName,
                        LastName = editVM.Publisher.LastName,
                        Biography = editVM.Publisher.Biography,
                        DateOfBrith = editVM.Publisher.DateOfBrith
                    }
                };

                _bookRepository.Update(book);
                return RedirectToAction("BooksAdminPanel");
            }
            else
            {
                // Obsługa przypadku, gdy IdPublisher lub Publisher są null
                ModelState.AddModelError("Publisher", "Publisher is required.");
                return View("Edit", editVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)  return View("Error");

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null) return View("Error");

            _bookRepository.Delete(book);
            return RedirectToAction("BooksAdminPanel");
        }
    }
}
