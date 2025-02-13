using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class ImageCreationException : Exception
    {
        public ImageCreationException(string message) : base(message) { }
    }
}
