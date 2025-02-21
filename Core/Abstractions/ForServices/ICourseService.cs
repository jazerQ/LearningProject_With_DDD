using DataAccess.Entities;

namespace Application
{
    public interface ICourseService
    {
        Task DeleteAsync(Guid id, Guid authorId);
        Task<List<CourseEntity>> Get();
        Task<List<CourseEntity>> GetWithLessons();
        Task UpdateAsync(Guid id, Guid authorId, string title, string description, decimal price);
        Task WriteAsync(Guid id, Guid authorId, string title, string description, decimal price);
    }
}