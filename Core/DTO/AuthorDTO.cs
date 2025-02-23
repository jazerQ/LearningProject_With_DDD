using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
