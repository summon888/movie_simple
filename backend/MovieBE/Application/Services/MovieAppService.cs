using Application.EventSourcedNormalizers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Core.Bus;
using Domain.Customers.Commands;
using Domain.Customers.Interfaces;
using Domain.Customers.Specifications;
using Domain.Movies.Commands;
using Domain.Movies.Interfaces;
using Domain.Movies.Specifications;
using Infrastructure.Repository.EventSourcing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MovieAppService : IMovieAppService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IMediatorHandler _bus;

        public MovieAppService(
            IMapper mapper,
            IMovieRepository movieRepository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _bus = bus;
        }

        public IEnumerable<MovieViewModel> GetAll()
        {
            return _movieRepository.GetAll().ProjectTo<MovieViewModel>(_mapper.ConfigurationProvider);
        }

        public IEnumerable<MovieViewModel> GetAll(int skip, int take)
        {
            return _movieRepository.GetAll(new MovieFilterPaginatedSpecification(skip, take))
                .ProjectTo<MovieViewModel>(_mapper.ConfigurationProvider);
        }

        public MovieViewModel GetById(Guid id)
        {
            return _mapper.Map<MovieViewModel>(_movieRepository.GetById(id));
        }

        public void Add(MovieViewModel movieViewModel)
        {
            var addCommand = _mapper.Map<AddNewMovieCommand>(movieViewModel);
            _bus.SendCommand(addCommand);
        }

        public void Update(MovieViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateMovieCommand>(customerViewModel);
            _bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveMovieCommand(id);
            _bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
