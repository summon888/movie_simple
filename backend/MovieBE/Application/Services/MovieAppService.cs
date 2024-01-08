using Application.EventSourcedNormalizers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Core.Bus;
using Domain.Customers.Commands;
using Domain.Customers.Interfaces;
using Domain.Customers.Specifications;
using Domain.Likes.Commands;
using Domain.Likes.Interfaces;
using Domain.Movies.Commands;
using Domain.Movies.Entities;
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
        private readonly ILikeRepository _likeRepository;
        private readonly IMediatorHandler _bus;

        public MovieAppService(
            IMapper mapper,
            IMovieRepository movieRepository,
            ILikeRepository likeRepository,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _likeRepository = likeRepository;
            _bus = bus;
        }

        public IEnumerable<MovieViewModel> GetAll()
        {
            return _movieRepository.GetAll().ProjectTo<MovieViewModel>(_mapper.ConfigurationProvider);
        }

        public List<MovieViewModel> GetAll(Guid customerId, int skip, int take)
        {
            var movies = _movieRepository.GetAll(new MovieFilterPaginatedSpecification(skip, take))
                .ProjectTo<MovieViewModel>(_mapper.ConfigurationProvider).ToList();
            //đoạn này có thể xử lý bằng relation nhưng cấu hình đang bị lỗi 
            #region 
            var listIds = movies.Select(a => a.Id).ToList();

            var listLikesAndDisLikes = _likeRepository.GetLike(listIds);

            foreach (var movie in movies)
            {
                var listLike = listLikesAndDisLikes.Where(a => a.MovieId == movie.Id && a.IsLike);
                var listDisLike = listLikesAndDisLikes.Where(a => a.MovieId == movie.Id && !a.IsLike);
                movie.TotalLike = listLike.Count();
                movie.TotalDislikes = listDisLike.Count();
                movie.Liked = listLike.Any(a => a.CustomerId == customerId && a.IsLike);
                movie.DisLiked = listDisLike.Any(a => a.CustomerId == customerId && !a.IsLike);
            }
            #endregion

            return movies;
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

        public void Like(MovieLikeViewModel movieLikeViewModel)
        {
            var addCommand = _mapper.Map<AddLikeCommand>(movieLikeViewModel);
            _bus.SendCommand(addCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
