using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public NewsEntity? News { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
