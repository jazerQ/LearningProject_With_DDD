using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.EducationPlatform;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly LearningCoursesDbContext _context;
        public LessonRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }
        public async Task WriteLesson(Guid courseId, LessonEntity lesson)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new Exception("Error! Not Found your essential course");
            course.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task WriteLesson2(Guid courseId, string title, string description, string lessonText)
        {
            var lesson = new LessonEntity()
            {
                Title = title,
                Description = description,
                LessonText = lessonText,
                CourseId = courseId
            };
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task<List<LessonEntity>> GetLessonsAsync(Guid courseId)
        {
            var lessons = await _context.Lessons.Where(l => l.CourseId == courseId).Select(l => l).ToListAsync();
            return lessons;
        }
    }
}
