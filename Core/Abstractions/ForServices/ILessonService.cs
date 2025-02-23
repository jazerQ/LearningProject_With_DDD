using DataAccess.Entities;

namespace Application
{
    public interface ILessonService
    {
        Task<List<LessonEntity>> GetLessons(Guid courseId);
        Task WriteLesson(Guid courseId, string title, string description, string lessonText);
    }
}