using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
     
        public async Task<IEnumerable<Book>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Book book)
        {
            _context.Add(book);
            return true;
        }

        public bool Delete(Book book)
        {
            _context.Remove(book);
            return true;
        }
        public bool Update(Book book)
        {
            _context.Update(book);
            return true;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
