using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class ImageSavingIntoDbException : Exception
    {
        public ImageSavingIntoDbException(int id, int newsId, string filename) : base($"While saving into db an error occurred  Info about this Entity Id = {id} newsId = {newsId} filename = {filename}") 
        {
            this.Id = id;
            this.NewsId = newsId;
            this.FileName = filename;
        }
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
        
}
