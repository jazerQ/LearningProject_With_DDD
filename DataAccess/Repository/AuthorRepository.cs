using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LearningCoursesDbContext _context;
        public AuthorRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }
        public int GetLastValueOfAuthorId()
        {
            return _context.Authors.Max(a => a.Id) + 1;
        }
    }
}
