using Core.Enums;
using Core.Models;

namespace Core.Abstractions.ForRepositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(Guid id);
    }
}