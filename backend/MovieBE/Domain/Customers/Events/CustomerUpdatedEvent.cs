using Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid id, string displayname, string email)
        {
            Id = id;
            DisplayName = displayname;
            Email = email;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string DisplayName { get; private set; }

        public string Email { get; private set; }
    }
}
