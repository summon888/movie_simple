using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Providers.Http
{
    public class Response
    {
        public string Version { get; set; }

        public int StatusCode { get; set; }

        public string RequestId { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
