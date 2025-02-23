using DataAccess.Entities;

namespace DataAccess.Repository
{
    public interface ILessonRepository
    {
        Task<List<LessonEntity>> GetLessonsAsync(Guid courseId);
        Task WriteLesson2(Guid courseId, string title, string description, string lessonText);
    }
}