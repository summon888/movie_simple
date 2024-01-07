using Application.ViewModels;
using AutoMapper;
using Domain.Customers.Commands;
using Domain.Likes.Commands;
using Domain.Movies.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //Customer
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.DisplayName, c.Email));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.DisplayName, c.Email));
            //Movie
            CreateMap<MovieViewModel, AddNewMovieCommand>()
                .ConstructUsing(c => new AddNewMovieCommand(c.Title, c.Description, c.ThumbnailURL, c.Author));
            CreateMap<MovieViewModel, UpdateMovieCommand>()
                .ConstructUsing(c => new UpdateMovieCommand(c.Id, c.Title, c.Description, c.ThumbnailURL, c.Author));
            //Like
            CreateMap<MovieLikeViewModel, AddLikeCommand>()
                .ConstructUsing(c => new AddLikeCommand(c.MovieId, c.CustomerId, c.IsLike));
        }
    }
}
