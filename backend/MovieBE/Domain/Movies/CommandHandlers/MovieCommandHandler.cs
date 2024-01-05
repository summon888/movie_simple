using Domain.Core.Bus;
using Domain.Core.CommandHandlers;
using Domain.Core.Interfaces;
using Domain.Core.Notifications;
using Domain.Customers.Commands;
using Domain.Customers.Entities;
using Domain.Customers.Events;
using Domain.Movies.Commands;
using Domain.Movies.Entities;
using Domain.Movies.Events;
using Domain.Movies.Interfaces;
using MediatR;

namespace Domain.Customers.CommandHandlers
{
    public class MovieCommandHandler : CommandHandler,
    IRequestHandler<AddNewMovieCommand, bool>,
    IRequestHandler<UpdateMovieCommand, bool>,
    IRequestHandler<RemoveMovieCommand, bool>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMediatorHandler _bus;

        public MovieCommandHandler(
            IMovieRepository movieRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _movieRepository = movieRepository;
            _bus = bus;
        }

        public Task<bool> Handle(AddNewMovieCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var movie = new Movie(Guid.NewGuid(), message.Title, message.Description, message.ThumbnailURL, message.Author);

            if (_movieRepository.GetByTitle(movie.Title) != null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "The movie title has already been taken."));
                return Task.FromResult(false);
            }

            _movieRepository.Add(movie);

            if (Commit())
            {
                _bus.RaiseEvent(new MovieAddNewEvent(movie.Id, movie.Title, movie.Description, movie.ThumbnailUrl, movie.Author));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateMovieCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var movie = new Movie(message.Id, message.Title, message.Description, message.ThumbnailURL, message.Author);
            var existingMovie = _movieRepository.GetByTitle(movie.Title);

            if (existingMovie != null && existingMovie.Id != movie.Id)
            {
                if (!existingMovie.Equals(movie))
                {
                    _bus.RaiseEvent(new DomainNotification(message.MessageType, "The movie title has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _movieRepository.Update(movie);

            if (Commit())
            {
                _bus.RaiseEvent(new MovieUpdatedEvent(movie.Id, movie.Title, movie.Description, movie.ThumbnailUrl, movie.Author));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveMovieCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _movieRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new CustomerRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _movieRepository.Dispose();
        }
    }
}
