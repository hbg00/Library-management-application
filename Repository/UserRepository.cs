using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User?>> GetUserByPesel(string pesel)
        {
            return await _context.Users.Where(u => u.Pesel.Contains(pesel)).ToListAsync();
        }
        public bool Add(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(User user)
        {
            _context.Remove(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
