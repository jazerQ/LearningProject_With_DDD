using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
        public CourseEntity? Course { get; set; }
    }
}
