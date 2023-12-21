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
        public async Task<IActionResult> Create()
        {

            var bookVM = new CreateBookViewModel();
          

            return View(bookVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel bookVM)
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
                        DateOfBrith = bookVM.DateOfBirth // Corrected property name
                    }
                };

                _bookRepository.Add(book);
                return RedirectToAction("Index");
            }
         
            return View(bookVM);
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return View();
        }

    }

}
