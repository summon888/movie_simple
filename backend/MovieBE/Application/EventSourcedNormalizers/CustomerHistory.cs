﻿using Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.EventSourcedNormalizers
{
    public class CustomerHistory
    {
        public static IList<CustomerHistoryData> HistoryData { get; set; }

        public static IList<CustomerHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CustomerHistoryData>();
            CustomerHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CustomerHistoryData>();
            var last = new CustomerHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CustomerHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? string.Empty
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? string.Empty
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? string.Empty
                        : change.Email,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? string.Empty : change.Action,
                    When = change.When,
                    Who = change.Who,
                };

                list.Add(jsSlot);
                last = change;
            }

            return list;
        }

        private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CustomerHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CustomerRegisteredEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerUpdatedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CustomerRemovedEvent":
                        values = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }

                HistoryData.Add(slot);
            }
        }
    }
}
