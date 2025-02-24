using Core.DTO;
using DataAccess.Entities;

namespace DataAccess.Repository
{
    public interface ILessonRepository
    {
        Task<List<LessonEntity>> GetLessonsAsync(Guid courseId);
        Task WriteLesson2(Guid courseId, string title, string description, string lessonText);
        Task Update(Guid lessonId, string title, string description, string lessonText, Guid courseId);
        Task Delete(Guid lessonId, Guid courseId);
        Task<LessonDTO> GetLessonById(Guid lessonId);
    }
}