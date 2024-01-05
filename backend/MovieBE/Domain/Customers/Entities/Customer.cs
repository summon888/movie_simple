using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Entities
{
    public class Customer : EntityAudit
    {
        public Customer() { }

        public Customer(Guid id, string email, string displayname) 
        {
            Id = id;
            Email = email;
            DisplayName = displayname;
        }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}
