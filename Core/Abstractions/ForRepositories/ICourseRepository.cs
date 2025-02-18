using DataAccess.Entities;

namespace DataAccess.Repository
{
    public interface ICourseRepository
    {
        Task DeleteEntity(int id);
        Task<List<CourseEntity>> Get();
        Task<List<CourseEntity>> GetWithFilter(string title, decimal price = -1);
        Task<CourseEntity?> GetWithId(int id);
        Task<List<CourseEntity>> GetWithLessons();
        Task<List<CourseEntity>> GetWithPagination(int page, int pageSize);
        Task UpdateValue(int id, string title, string description, decimal price);
        Task UpdateValueSecondMethod(int id, string title, string description, decimal price);
        Task WriteValue(int id,int authorId, string title, string description, decimal price);
    }
}