using Application.ViewModels;
using AutoMapper;
using Domain.Customers.Entities;
using Domain.Likes.Entities;
using Domain.Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Movie, MovieViewModel>();
            CreateMap<Like, MovieLikeViewModel>();
        }
    }
}
