using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<List<CourseEntity>> Get()
        {
            var CoursesEntity = await _courseRepository.Get();
            return CoursesEntity;
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
