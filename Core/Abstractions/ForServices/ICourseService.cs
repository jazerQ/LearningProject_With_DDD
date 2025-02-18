using DataAccess.Entities;

namespace Application
{
    public interface ICourseService
    {
        Task DeleteAsync(int id);
        Task<List<CourseEntity>> Get();
        Task<List<CourseEntity>> GetWithLessons();
        Task UpdateAsync(int id, string title, string description, decimal price);
        Task WriteAsync(int id, int authorId, string title, string description, decimal price);
    }
}