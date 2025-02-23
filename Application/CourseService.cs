using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Exceptions;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Application
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<List<CourseDTO>> Get()
        {
            var CoursesEntity = await _courseRepository.Get();
            return CoursesEntity;
        }
        public async Task<Guid> GetCourseIdByUserId(Guid userId) 
        {
            try
            {
                var courseId = await _courseRepository.GetCourseIdByUserId(userId);
                return courseId;
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex) {
                throw;
            }
        }
        public async Task<List<CourseEntity>> GetWithLessons()
        {
            var CoursesEntity = await _courseRepository.GetWithLessons();
            return CoursesEntity;
        }
        public async Task WriteAsync(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _courseRepository.WriteValue(id, authorId, title, description, price);
        }
        public async Task UpdateAsync(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _courseRepository.UpdateValueSecondMethod(id, authorId, title, description, price);
        }
        public async Task DeleteAsync(Guid id, Guid authorId)
        {
            await _courseRepository.DeleteEntity(id, authorId);
        }
    }
}
