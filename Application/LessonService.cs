using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repository;

namespace Application
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public async Task WriteLesson(Guid courseId, string title, string description, string lessonText)
        {
            await _lessonRepository.WriteLesson2(courseId, title, description, lessonText);
        }
        public async Task<List<LessonEntity>> GetLessons(Guid courseId)
        {
            var listOfLessons = await _lessonRepository.GetLessonsAsync(courseId);
            return listOfLessons;
        }
    }
}
