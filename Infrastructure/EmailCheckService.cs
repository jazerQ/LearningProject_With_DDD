using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class EmailCheckService : IEmailCheckService
    {
        private readonly string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public bool EmailIsValid(string email)
        {
            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
