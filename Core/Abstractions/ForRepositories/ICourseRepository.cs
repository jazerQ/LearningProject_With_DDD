using Core.DTO;
using DataAccess.Entities;

namespace DataAccess.Repository
{
    public interface ICourseRepository
    {
        Task DeleteEntity(Guid id, Guid authorId);
        Task<List<CourseDTO>> Get();
        Task<Guid> GetCourseIdByUserId(Guid userId);
        Task<List<CourseEntity>> GetWithFilter(string title, decimal price = -1);
        Task<List<CourseEntity>> GetWithLessons();
        Task<List<CourseEntity>> GetWithPagination(int page, int pageSize);
        Task<List<CourseEntity>> GetWithTitle(string title);
        Task UpdateValue(Guid id, Guid authorId, string title, string description, decimal price);
        Task UpdateValueSecondMethod(Guid id, Guid authorId, string title, string description, decimal price);
        Task WriteValue(Guid id, Guid authorId, string title, string description, decimal price);
        Task JoinToCourse(Guid courseId, Guid userId);
        Task<CourseDTO> GetMyManagedCourse(Guid userId);
        Task<List<CourseDTO>> GetMyCourses(Guid userId);
        Task Update(Guid courseId, string title, string descriptions, decimal price);
        Task Delete(Guid courseId);
    }
}