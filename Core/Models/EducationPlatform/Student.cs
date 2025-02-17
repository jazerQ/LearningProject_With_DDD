using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models.EducationPlatform
{
    public class Student
    {
        public Student(int id, string username)
        {
            this.Id = id;
            this.Username = username;
        }
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        private readonly List<Course> _courses = new();
        public IReadOnlyCollection<Course> Courses => _courses;
        public void AddValue(List<Course> courses) => _courses.AddRange(courses);
        private static readonly string pattern = "\b(nigger | jew | fag)\b";
        public static Result<Student> Create(int id, string username) 
        {
            if (string.IsNullOrEmpty(username) || Regex.IsMatch(username, pattern, RegexOptions.IgnoreCase)) 
            {
                return Result.Failure<Student>("your username is empty or offensive");
            }
            Student student = new Student(id, username);
            return Result.Success<Student>(student);
        }
    }
}
