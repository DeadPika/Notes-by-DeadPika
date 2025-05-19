using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastructure.Authentication
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; } = string.Empty;
        public int ExpiresHours { get; set; }
    }
}
