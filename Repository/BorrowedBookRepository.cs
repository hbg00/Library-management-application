using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowedBookRepository(LibraryDbContext context)
        {
            _context = context; 
        }
        public async Task<BorrowedBook?> GetByIdAsync(int id)
        {
            return await _context.BorrowedBooks.
                FirstOrDefaultAsync(bB => bB.Id == id);   
        }
        public async Task<BorrowedBook?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.BorrowedBooks
                .AsNoTracking()
                .FirstOrDefaultAsync(bB => bB.Id == id);
        }
        public bool Add(BorrowedBook borrowedBook)
        {
            _context.Add(borrowedBook);
            return Save();
        }

        public bool Delete(BorrowedBook borrowedBook)
        {
            _context.Remove(borrowedBook);
            return Save();
        }

        public bool Update(BorrowedBook borrowedBook)
        {
            _context.Update(borrowedBook);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

       
    }
}
