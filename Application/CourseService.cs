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
        public async Task JoinToCourse(Guid courseId, Guid userId) 
        {
            try
            {
                await _courseRepository.JoinToCourse(courseId, userId);
            }
            catch (EntityNotFoundException ex)
            {
                throw;

            }
            catch (AlreadyExistException ex) 
            {
                throw;
            }
            catch(Exception ex) 
            {
                throw;
            }
        }
        public async Task<CourseDTO> GetManagedCourse(Guid userId) 
        {
            try
            {
                CourseDTO courseDTO = await _courseRepository.GetMyManagedCourse(userId);
                return courseDTO;
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public async Task<List<CourseDTO>> GetMyCourses(Guid userId) 
        {
            try
            {
                return await _courseRepository.GetMyCourses(userId);
            } catch (Exception ex) {
                throw;
            }
        }
        public async Task Update(Guid courseId, string title, string description, decimal price)
        {
            try
            {
                await _courseRepository.Update(courseId, title, description, price);
            }catch(Exception ex) 
            {
                throw;
            }
        }
        public async Task Delete(Guid courseId)
        {
            try
            {
                await _courseRepository.Delete(courseId);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
