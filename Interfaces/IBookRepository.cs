using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<IEnumerable<Book>> GetBooksByName(string name);
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIdAsyncNoTracking(int id);
        Task<Book?> GetByIdAsyncForNumberOfCopies(int id);
        Task<Book?> GetByIdAsyncForNumberOfCopiesNoTracking(int id);
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        bool Save();
    }
}
