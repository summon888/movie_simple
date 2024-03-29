﻿using Domain.Core.Commands;
using Domain.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command)
            where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event)
            where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
            {
                _eventStore?.Save(@event);
            }

            return _mediator.Publish(@event);
        }
    }
}
