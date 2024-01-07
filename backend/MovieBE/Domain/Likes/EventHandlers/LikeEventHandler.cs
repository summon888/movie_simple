using Domain.Likes.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.EventHandlers
{
    public class LikeEventHandler : INotificationHandler<AddLikeEvent>
    {
        public Task Handle(AddLikeEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
