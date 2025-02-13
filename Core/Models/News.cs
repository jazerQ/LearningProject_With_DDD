using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models
{
    public class News
    {
        public const int MAX_LENGTH_TITLE = 32;
        private readonly List<Image> _news = new();
        public News(int id, string title, string textData, DateTime createdDate, Image? image, int views = 0)
        {
            this.Id = id;
            this.Title = title;
            this.TextData = textData;
            this.CreatedDate = createdDate;
            this.TitleImage = image;
            this.Views = views;
        }
        public int Id { get; }
        public string Title { get; } = string.Empty;
        public string TextData { get; } = string.Empty;
        public DateTime CreatedDate { get; }
        public int Views { get; private set; } = 0;
        public Image? TitleImage { get; }
        public IReadOnlyCollection<Image> ImageList => _news;
        public void AddImages(List<Image> images) => _news.AddRange(images);
        public static Result<News> Create(int id, string title, string textData, Image? image, DateTime? dateTime, int Views = 0) 
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_LENGTH_TITLE) 
            {
                return Result.Failure<News>("Title is Empty or string length is greater than maximum");
            }
            if (string.IsNullOrEmpty(textData)) 
            {
                return Result.Failure<News>("Text From News are Empty");
            }
            if (dateTime.HasValue) 
            {
                var newsf = new News(id, title, textData, dateTime.Value, image, Views);
                return Result.Success<News>(newsf);
            }
            var news = new News(id, title, textData, DateTime.UtcNow, image, Views);

            return Result.Success<News>(news);
        }
        public void AddView() 
        {
            Views++;
        }
    }
}
