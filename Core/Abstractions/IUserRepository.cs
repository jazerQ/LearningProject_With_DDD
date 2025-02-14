using Core.Models;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}