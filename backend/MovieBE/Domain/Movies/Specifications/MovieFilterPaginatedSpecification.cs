using Domain.Core.Specifications;
using Domain.Movies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Movies.Specifications
{
    public class MovieFilterPaginatedSpecification : BaseSpecification<Movie>
    {
        public MovieFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
