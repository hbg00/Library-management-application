using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryDbContext _context;
        public PublisherRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await _context.Publishers
                .ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers
                .FirstOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<Publisher?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Publishers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Publisher>> GetPublisherByName(string name)
        {
            return await _context.Publishers
                .Where(p => p.FirstName.Contains(name))
                .ToListAsync();
        }

        public bool Delete(Publisher book)
        {
            _context.Remove(book);
            return Save();
        }

        public bool Update(Publisher book)
        {
            _context.Update(book);
            return Save();

        }
        public bool Save()
        {
            var saved =_context.SaveChanges();
            return saved > 0;
        }
    }
}
