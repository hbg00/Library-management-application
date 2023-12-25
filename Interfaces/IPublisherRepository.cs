using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAll();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher?> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Publisher>> GetPublisherByName(string name);
        bool Update(Publisher book);
        bool Delete(Publisher book);
        bool Save();
    }
}
