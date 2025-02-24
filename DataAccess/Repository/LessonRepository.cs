using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Exceptions;
using Core.Models.EducationPlatform;
using DataAccess.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly LearningCoursesDbContext _context;
        private readonly IMailSenderService _mailSender;
        public LessonRepository(LearningCoursesDbContext context, IMailSenderService mailSender)
        {
            _context = context;
            _mailSender = mailSender;
        }
        public async Task WriteLesson(Guid courseId, LessonEntity lesson)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new Exception("Error! Not Found your essential course");
            course.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task WriteLesson2(Guid courseId, string title, string description, string lessonText)
        {
            var studentsId = await _context.Students.Include(s => s.Courses).Where(s => s.Courses.Any(c => c.Id == courseId)).Select(s => s.Id).ToListAsync();
            var users = await _context.Users.Where(u => studentsId.Any(s => s == u.Id)).Select(u => u).ToListAsync();
            var courseName = await _context.Courses.Where(c => c.Id == courseId).Select(c => c.Title).FirstOrDefaultAsync();
            foreach (var user in users) {
                await _mailSender.SendEmailAsync(user.Email, $"\"{courseName}\" у этого курса новый урок!", $"Привет, {user.Username} новый урок появился с названием {title}");
            }
            var lesson = new LessonEntity()
            {
                Id = Guid.NewGuid(),
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
        private async Task<bool> IsOwnLesson(Guid courseId, Guid lessonId) 
        {
            return await _context.Lessons.Where(l => l.Id == lessonId && l.CourseId == courseId).SingleOrDefaultAsync() != null;
        }
        public async Task Update(Guid lessonId, string title, string description, string lessonText, Guid courseId)
        {
            try
            {
                if (await IsOwnLesson(courseId, lessonId) == false) throw new NotYourLessonException($"курс с id: {courseId} вам не доступен, так как вы не являетесь автором курса");
                await _context.Lessons.Where(l => l.CourseId == courseId && l.Id == lessonId).ExecuteUpdateAsync(l => l.SetProperty(ls => ls.Title, title)
                                                                                             .SetProperty(ls => ls.Description, description)
                                                                                             .SetProperty(ls => ls.LessonText, lessonText));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task Delete(Guid lessonId,Guid courseId) 
        {
            try
            {
                if (await IsOwnLesson(courseId, lessonId) == false) throw new NotYourLessonException($"курс с id: {courseId} вам не доступен, так как вы не являетесь автором курса");
                await _context.Lessons.Where(l => l.Id == lessonId && l.CourseId == courseId).ExecuteDeleteAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<LessonDTO> GetLessonById(Guid lessonId) 
        {
            try
            {
                return await _context.Lessons.Where(l => l.Id == lessonId).Select(c => new LessonDTO { Id = lessonId, Description = c.Description, LessonText = c.LessonText, Title = c.Title }).SingleOrDefaultAsync() ?? throw new EntityNotFoundException($"NotFound lesson with id \"{lessonId}\"");
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
