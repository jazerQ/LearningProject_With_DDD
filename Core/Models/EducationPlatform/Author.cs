using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models.EducationPlatform
{
    public class Author
    {
        public Author(int id, string username, int courseId, Course? course)
        {
            this.Id = id;
            this.Username = username;
            this.CourseId = courseId;
            this.Course = course;
        }
        public int Id { get; private set; }
        public string Username { get; private set; } = string.Empty;

        public int CourseId { get; private set; }
        public Course? Course { get; private set; }
        private static readonly string pattern = "\b(nigger | jew | fag)\b";

        public static Result<Author> Create(int id, string username, int courseId, Course? course) 
        {
            
            if (string.IsNullOrEmpty(username) || Regex.IsMatch(username, pattern, RegexOptions.IgnoreCase)) 
            {
                return Result.Failure<Author>("your username is empty or offensive");
            }
            Author author = new Author(id, username, courseId, course);
            return Result.Success<Author>(author);
        }
    }
}
