using Domain.Customers.Entities;
using Domain.Customers.Interfaces;
using Domain.Movies.Entities;
using Domain.Movies.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Movies
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Movie GetByAuthor(string author)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Author == author);
        }

        public Movie GetByTitle(string title)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(c => c.Title == title);
        }
    }
}
