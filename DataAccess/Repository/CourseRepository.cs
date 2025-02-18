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
    public class CourseRepository : ICourseRepository
    {
        private readonly LearningCoursesDbContext _context;
        public CourseRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseEntity>> Get()
        {

            return await _context.Courses
                .AsNoTracking()// чтобы не отслеживалось, оптимизирует запрос в бд
                .OrderBy(c => c.Price)   // т.о происходит сортировка по цене
                .ToListAsync();
        }

        public async Task<List<CourseEntity>> GetWithLessons()
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Lessons)
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetWithId(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<CourseEntity>> GetWithFilter(string title, decimal price = -1)
        {
            var query = _context.Courses.AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                query.Where(c => c.Title.Contains(title));
            }
            if (price != -1)
            {
                query.Where(c => c.Price > price);
            }
            return await query.ToListAsync();
        }

        public async Task<List<CourseEntity>> GetWithPagination(int page, int pageSize)
        {
            return await _context.Courses
                                .AsNoTracking()
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        }

        public async Task WriteValue(int id, int authorId, string title, string description, decimal price)
        {
            AuthorEntity authorEntity = new AuthorEntity()
            {
                Id = authorId,
                Username = "kostil",
                CourseId = id
            };
            CourseEntity course = new CourseEntity()
            {
                Id = id,
                AuthorId = authorId,
                Title = title,
                Description = description,
                Price = price
            };
            await _context.Authors.AddAsync(authorEntity);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateValue(int id, string title, string description, decimal price)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("Error! application cant find this entity");
            course.Title = title;
            course.Description = description;
            course.Price = price;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateValueSecondMethod(int id, string title, string description, decimal price)
        {
            await _context.Courses
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                         .SetProperty(c => c.Title, title)
                         .SetProperty(c => c.Description, description)
                         .SetProperty(c => c.Price, price)
                );

        }
        public async Task DeleteEntity(int id)
        {
            await _context.Courses.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
    }


}
