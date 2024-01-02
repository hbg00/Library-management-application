using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBorrowedBookRepository
    {
        Task<BorrowedBook?> GetByIdAsyncNoTracking(int id);
        Task<BorrowedBook?> GetByIdAsync(int id);
        bool Add(BorrowedBook borrowedBook);
        bool Update(BorrowedBook borrowedBook);
        bool Delete(BorrowedBook borrowedBook);
        bool Save();
    }
}
