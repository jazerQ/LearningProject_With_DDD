using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<CourseEntity> Courses { get; set; } = [];
    }
}
