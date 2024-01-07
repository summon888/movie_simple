using Domain.Core.Bus;
using Domain.Core.CommandHandlers;
using Domain.Core.Events;
using Domain.Core.Interfaces;
using Domain.Core.Notifications;
using Domain.Likes.Commands;
using Domain.Likes.Entities;
using Domain.Likes.Events;
using Domain.Likes.Interfaces;
using Domain.Movies.Entities;
using Domain.Movies.Events;
using Domain.Movies.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.CommandHandlers
{
    public class LikeCommandHandler : CommandHandler, IRequestHandler<AddLikeCommand, bool>
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMediatorHandler _bus;
        public LikeCommandHandler(ILikeRepository likeRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) 
            : base(uow, bus, notifications)
        {
            _likeRepository = likeRepository;
            _bus = bus;
        }

        public Task<bool> Handle(AddLikeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }
            
            if (_likeRepository.GetLike(message.MovieId, message.CustomerId, message.IsLike) != null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "The like has already been taken."));
                return Task.FromResult(false);
            }

            var like = new Like(Guid.NewGuid(), message.MovieId, message.CustomerId, message.IsLike);

            _likeRepository.Add(like);

            if (Commit())
            {
                _bus.RaiseEvent(new AddLikeEvent(like.Id, like.MovieId, like.CustomerId, like.IsLike));
            }

            return Task.FromResult(true);
        }
    }
}
