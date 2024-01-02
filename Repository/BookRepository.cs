using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IPublisherRepository _publisherRepository;

        public BookRepository(LibraryDbContext context, IPublisherRepository publisherRepository)
        {
            _context = context;
            _publisherRepository = publisherRepository;

        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books
                .Include(p => p.Publisher)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByName(string name)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(name))
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByNameAdmin(string name) 
        {
            return await _context.Books
                    .Where(b => b.Title.Contains(name))
                    .Include(p => p.Publisher)
                    .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books
                .Include(p => p.Publisher)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByIdAsyncForNumberOfCopies(int id) 
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByIdAsyncForNumberOfCopiesNoTracking(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public bool Add(Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool Delete(Book book)
        {
            _publisherRepository.Delete(book.Publisher);
            _context.Remove(book);
            return Save();
        }

        public bool Update(Book book)
        {
            _context.Update(book);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
