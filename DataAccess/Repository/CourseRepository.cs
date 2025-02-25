using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Exceptions;
using Core.Models.EducationPlatform;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DataAccess.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LearningCoursesDbContext _context;
        public CourseRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseDTO>> Get()
        {

            var courses  = await _context.Courses
                .AsNoTracking()// чтобы не отслеживалось, оптимизирует запрос в б 
                .Include(c => c.Lessons)
                .Include(c => c.Students)
                .Include(c => c.Author)
                .Select(c => c)
                .ToListAsync();
            List<CourseDTO> coursesDTO = courses.Select(c => new CourseDTO { 
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                Author = c.Author != null ? new AuthorDTO { Id = c.AuthorId, Username = c.Author.Username } : null,
                Lessons = c.Lessons.Select(l => new LessonDTO { Id = l.Id, Description = l.Description, LessonText = l.LessonText, Title = l.Title }).ToList(),
                Students = c.Students.Select(s => new StudentsDTO { Id = s.Id, Username = s.Username}).ToList()
                })
                .ToList();
            return coursesDTO;

        }
        public async Task<Guid> GetCourseIdByUserId(Guid userId) 
        {
            try
            {
                var courseId = await _context.Courses.FirstOrDefaultAsync(c => c.AuthorId == userId) ?? throw new EntityNotFoundException("I not found your courses");
                return courseId.Id;
            }
            catch (EntityNotFoundException ex)
            {
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
                Username = user.Username, 
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
        public async Task JoinToCourse(Guid courseId, Guid userId) 
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new EntityNotFoundException("I cant found this user");
                var student = await _context.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.Id == userId);
                if (student == null)
                {
                    StudentEntity newStudent = new StudentEntity { Id = userId, Username = user.Username };
                    await _context.Students.AddAsync(newStudent);
                    await _context.SaveChangesAsync();
                    student = newStudent;
                }
                if (student.Courses.Any(c => c.Id == courseId))
                {
                    throw new AlreadyExistException($"{user.Username} with id: {user.Id} Already consist in this course {courseId}");
                }
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new EntityNotFoundException("I cant found this course");
                student.Courses.Add(course);
                course.Students.Add(student);
                await _context.SaveChangesAsync();
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (AlreadyExistException ex)
            {
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task<CourseDTO> GetMyManagedCourse(Guid userId) 
        {
            try
            {
                Guid courseId = await GetCourseIdByUserId(userId);
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new EntityNotFoundException("у вас нет на данный момент курсов");
                var courseDto = new CourseDTO
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Price = course.Price,
                    Author = new AuthorDTO
                    {
                        Id = userId,
                        Username = await _context.Users.Where(u => u.Id == userId).Select(u => u.Username).SingleOrDefaultAsync() ?? throw new EntityNotFoundException("пользователь не найден")
                    },
                    Lessons = await _context.Lessons.Where(l => l.CourseId == courseId).Select(l => new LessonDTO
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Description = l.Description,
                        LessonText = l.LessonText

                    }).ToListAsync(),
                    Students = await _context.Students.Include(s => s.Courses).Where(s => s.Courses.Any(c => c.Id == courseId)).Select(s => new StudentsDTO
                    {
                        Id = s.Id,
                        Username = s.Username
                    }).ToListAsync()
                };
                return courseDto;
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
        public async Task<List<CourseDTO>> GetMyCourses(Guid userId)
        {
            try
            {
                return await _context.Courses.Include(c => c.Students).Where(c => c.Students.Any(s => s.Id == userId)).Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    Author = _context.Authors
                                    .Where(a => a.CourseId == c.Id)
                                    .Select(a => new AuthorDTO
                                    {
                                        Id = a.Id,
                                        Username = a.Username
                                    }).SingleOrDefault(),
                }).ToListAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task Update(Guid courseId, string title, string descriptions, decimal price)
        {
            try
            {
                await _context.Courses.Where(c => c.Id == courseId).ExecuteUpdateAsync(c => c.SetProperty(cr => cr.Title, title)
                                                                                             .SetProperty(cr => cr.Description, descriptions)
                                                                                             .SetProperty(cr => cr.Price, price));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task Delete(Guid courseId) 
        {
            try
            {
                await _context.Authors.Where(a => a.CourseId == courseId).ExecuteDeleteAsync();
                await _context.Courses.Where(c => c.Id == courseId).ExecuteDeleteAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }


}
