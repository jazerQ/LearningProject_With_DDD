using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Entities
{
    public class NewsEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TextData { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int Views { get; set; } = 0;
        public int TitleImageId { get; set; } = 0;
        public ImageEntity? TitleImage { get; }

    }
}
