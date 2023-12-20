using BookStore.Interfaces;
using BookStore.Models;
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
        [Route("LibraryApp")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();

            return View(books);
        }

        [HttpGet]
        [Route("LibraryApp/{id}")]
        public async Task<IActionResult> DetailBook(int id) 
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? NotFound() : View(book);
        }
        
    }
}
