using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models.EducationPlatform
{
    public class Course
    {
        public Course(int id, string title, string description, decimal price, int authorId, Author? author)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Price = price;
            this.AuthorId = authorId;
            this.Author = author;
        }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;


        //////////////Lessons//////////////////
        private readonly List<Lesson> _lessons = new();
        public IReadOnlyCollection<Lesson> Lessons => _lessons;
        public void AddLessons(List<Lesson> lessons) => _lessons.AddRange(lessons);

        //////////Author/////////////
        public int AuthorId { get; private set; }
        public Author? Author { get; private set; }


        ///////////////Students////////
        private readonly List<Student> _students = new();
        public IReadOnlyCollection<Student> Students => _students;
        public void AddStudents(List<Student> students) => _students.AddRange(students);


        public static Result<Course> Create(int id, string title, string description, decimal price, int authorId, Author? author)
        {
            if (string.IsNullOrEmpty(title)) 
            {
                return Result.Failure<Course>("title can`t be empty");
            }
            if (string.IsNullOrEmpty(description)) 
            {
                return Result.Failure<Course>("description can`t be empty");
            }
            if (price < 0) 
            {
                return Result.Failure<Course>("price can`t be negative");
            }
            if(author == null) 
            {
                return Result.Failure<Course>("Author can`t be null");
            }
            Course course = new Course(id, title, description, price, authorId, author);
            return Result.Success<Course>(course);
        }
    }
}
