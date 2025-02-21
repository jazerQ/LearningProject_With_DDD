using DataAccess.Entities;

namespace DataAccess.Repository
{
    public interface IAuthorRepository
    {
        Task<AuthorEntity> GetWithId(Guid id);
    }
}