using Core.DTO;
using DataAccess.Entities;

namespace Application
{
    public interface ICourseService
    {
        Task DeleteAsync(Guid id, Guid authorId);
        Task<List<CourseDTO>> Get();
        Task<Guid> GetCourseIdByUserId(Guid userId);
        Task<List<CourseEntity>> GetWithLessons();
        Task UpdateAsync(Guid id, Guid authorId, string title, string description, decimal price);
        Task WriteAsync(Guid id, Guid authorId, string title, string description, decimal price);
        Task JoinToCourse(Guid courseId, Guid userId);
    }
}