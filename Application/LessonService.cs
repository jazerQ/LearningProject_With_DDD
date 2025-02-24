using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Exceptions;
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
        public async Task Update(Guid lessonId, string title, string description, string lessonText, Guid courseId) 
        {
            try
            {
                await _lessonRepository.Update(lessonId, title, description, lessonText, courseId);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public async Task Delete(Guid lessonId, Guid courseId) 
        {
            try
            {
                await _lessonRepository.Delete(lessonId, courseId);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public async Task<LessonDTO> GetLessonById(Guid lessonId) 
        {
            try
            {
                LessonDTO lesson = await _lessonRepository.GetLessonById(lessonId);
                return lesson;
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex) {
                throw;
            }
        }
    }
}
