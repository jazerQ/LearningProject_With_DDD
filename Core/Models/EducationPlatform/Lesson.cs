using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.EducationPlatform
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LessonText { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
