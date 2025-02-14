using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models
{
    public class Image
    {
        public Image(int id, string fileName)
        {
            this.Id = id;
            this.FileName = fileName;
        }
        public Image(string fileName) 
        {
            this.FileName = fileName;
        }
        public int Id { get; set; } = 0;
        public int NewsId { get; }
        public string FileName { get; set; } = string.Empty;

        public static Result<Image> Create(int id, string fileName) 
        {
            if (string.IsNullOrEmpty(fileName)) 
            {
                return Result.Failure<Image>("fileName is Empty");
            }
            var image = new Image(id, fileName);
            return Result.Success<Image>(image);
        }
        public static Result<Image> Create(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Result.Failure<Image>("fileName is Empty");
            }
            var image = new Image(fileName);
            return Result.Success<Image>(image);
        }
    }
}
