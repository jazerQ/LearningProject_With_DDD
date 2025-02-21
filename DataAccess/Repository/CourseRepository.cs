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
                .AsNoTracking()// чтобы не отслеживалось, оптимизирует запрос в б 
                .ToListAsync();
        }

        public async Task<List<CourseEntity>> GetWithLessons()
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Lessons)
                .ToListAsync();
        }

        public async Task<List<CourseEntity>> GetWithTitle(string title)
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(c => c.Title.Contains(title))
                .ToListAsync();
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

        public async Task WriteValue(Guid id, Guid authorId, string title, string description, decimal price)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == authorId) ?? throw new InvalidOperationException("User Not Found");
            AuthorEntity authorEntity = new AuthorEntity()
            {
                Id = authorId,
                Username = user.Username, // ТУТ КОСТЫЛЬ!
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

        public async Task UpdateValue(Guid id, Guid authorId, string title, string description, decimal price)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => (c.Id == id && c.AuthorId == authorId)) ?? throw new Exception("Error! application cant find this entity");
            course.Title = title;
            course.Description = description;
            course.Price = price;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateValueSecondMethod(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _context.Courses
                .Where(c => (c.Id == id && c.AuthorId == authorId))
                .ExecuteUpdateAsync(s => s
                         .SetProperty(c => c.Title, title)
                         .SetProperty(c => c.Description, description)
                         .SetProperty(c => c.Price, price)
                );

        }
        public async Task DeleteEntity(Guid id, Guid authorId)
        {
            await _context.Courses.Where(c => (c.Id == id && c.AuthorId == authorId)).ExecuteDeleteAsync();
        }
    }


}
