using Core.DTO;
using DataAccess.Entities;

namespace Application
{
    public interface ILessonService
    {
        Task<List<LessonEntity>> GetLessons(Guid courseId);
        Task WriteLesson(Guid courseId, string title, string description, string lessonText);
        Task Update(Guid lessonId, string title, string description, string lessonText, Guid courseId);
        Task Delete(Guid lessonId, Guid courseId);
        Task<LessonDTO> GetLessonById(Guid lessonId);
    }
}