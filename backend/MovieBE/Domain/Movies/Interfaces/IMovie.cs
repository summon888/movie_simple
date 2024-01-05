using Domain.Core.Interfaces;
using Domain.Customers.Entities;
using Domain.Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Movie GetByTitle(string title);
        Movie GetByAuthor(string author);
    }
}
