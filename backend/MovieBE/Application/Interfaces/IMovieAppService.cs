﻿using Application.EventSourcedNormalizers;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMovieAppService : IDisposable
    {
        void Add(MovieViewModel movieViewModel);

        IEnumerable<MovieViewModel> GetAll();

        List<MovieViewModel> GetAll(Guid customerId, int skip, int take);

        MovieViewModel GetById(Guid id);

        void Update(MovieViewModel movieViewModel);

        void Remove(Guid id);

        void Like(MovieLikeViewModel movieLikeViewModel);
    }
}
