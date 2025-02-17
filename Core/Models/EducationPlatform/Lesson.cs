using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models.EducationPlatform
{
    public class Lesson
    {
        public Lesson(int id, string title, string description, string lessonText, int courseId, Course? course)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.LessonText = lessonText;
            this.CourseId = courseId;
            this.Course = course;

        }
        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string LessonText { get; private set; } = string.Empty;

        public int CourseId { get; private set; }
        public Course? Course { get; private set; }

        public static Result<Lesson> Create(int id, string title, string description, string lessonText, int courseId, Course? course) 
        {
            if (string.IsNullOrEmpty(title)) 
            {
                return Result.Failure<Lesson>($"{nameof(title)} can`t be empty");
            }
            if (string.IsNullOrEmpty(description)) 
            {
                return Result.Failure<Lesson>($"{nameof(description)} can`t be empty");
            }
            if (string.IsNullOrEmpty(lessonText))
            {
                return Result.Failure<Lesson>("Text of lesson can`t be empty");
            }
            if (course == null) 
            {
                return Result.Failure<Lesson>("Course can`t be null");
            }
            Lesson lesson = new Lesson(id, title, description, lessonText, courseId, course);
            return Result.Success<Lesson>(lesson);
        }
    }
}
