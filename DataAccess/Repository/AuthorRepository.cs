using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LearningCoursesDbContext _context;
        public AuthorRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }
        public async Task<AuthorEntity> GetWithId(Guid id) 
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id) ?? throw new InvalidOperationException($"Not found Entity with Id {id}");
        }
    }
}
