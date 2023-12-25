using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User?>> GetAll();   
        Task<User?> GetById(string id);    
        Task<IEnumerable<User?>> GetUserByPesel(string pesel);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();

    }
}
