using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;   // секретный ключ который будет играть ключевую роль в сигнатуре
        public int ExpiresHours { get; set; } // время через которое секретный ключ обновится
    }
}
