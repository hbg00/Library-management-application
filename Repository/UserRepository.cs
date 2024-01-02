using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(LibraryDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<User?>> GetAll()
        {
            return await _userManager
                .GetUsersInRoleAsync(UserRoles.User);
        }

        public async Task<User?> GetById(string id)
        {
            return await _context.Users
                .Include(a => a.Address)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByIdAsyncNoTracking(string id)
        {
            return await _context.Users
                .Include(a => a.Address)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User?>> GetUserByPesel(string pesel)
        {
            var user = await _userManager
                .GetUsersInRoleAsync(UserRoles.User);

            return user.Where(user => user.Pesel.Contains(pesel));
        }

        public async Task<User?> GetDataForBorrowedBooks(User user)
        {
            return await _context.Users
                .Include(x => x.BorrowedBooks)
                .ThenInclude(b => b.Book)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
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

        public bool Update(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
