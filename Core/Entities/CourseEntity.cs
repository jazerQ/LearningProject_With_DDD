using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;

        public List<LessonEntity> Lessons { get; set; } = [];
        public int AuthorId { get; set; }
        public AuthorEntity? Author { get; set; }

        public List<StudentEntity> Students { get; set; } = [];
    }
}
