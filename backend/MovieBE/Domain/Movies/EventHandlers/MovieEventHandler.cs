using Domain.Customers.Events;
using Domain.Movies.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.EventHandlers
{
    public class MovieEventHandler :
    INotificationHandler<MovieAddNewEvent>,
    INotificationHandler<MovieUpdatedEvent>,
    INotificationHandler<MovieRemovedEvent>
    {
        public Task Handle(MovieUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(MovieAddNewEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(MovieRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
