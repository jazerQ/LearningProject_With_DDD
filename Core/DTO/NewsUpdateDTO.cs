using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.DTO
{
    public class NewsUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TextData { get; set; } = string.Empty;
        public Image? TitleImage { get; set; }
    }
}
