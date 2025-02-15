using Core.Models;

namespace Infrastructure
{
    public interface IJwtProvider
    {
        Task<string> GenerateToken(User user);
    }
}