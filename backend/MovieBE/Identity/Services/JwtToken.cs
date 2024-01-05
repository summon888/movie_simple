using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class JwtToken
    {
        public string JwtId { get; set; }

        public string AccessToken { get; set; }
    }
}
