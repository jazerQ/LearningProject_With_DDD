using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace Core.DTO
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;

        public List<LessonDTO> Lessons { get; set; } = [];
        public AuthorDTO? Author { get; set; }
        public List<StudentsDTO> Students { get; set; } = [];
    }
}
