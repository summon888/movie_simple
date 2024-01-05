using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventSourcedNormalizers
{
    public class CustomerHistoryData
    {
        public string Action { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string When { get; set; }

        public string Who { get; set; }
    }
}
