using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class NotValidEmailException : Exception
    {
        public NotValidEmailException(string message) : base(message){ }
    }
}
