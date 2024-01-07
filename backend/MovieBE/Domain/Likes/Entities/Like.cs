using Domain.Core.Models;
using Domain.Customers.Entities;
using Domain.Movies.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Entities
{
    public class Like : BaseEntity
    {
        public Like() { }

        public Like(Guid id, Guid movieId, Guid customerId, bool isLike) 
        { 
            Id = id;
            MovieId = movieId;
            CustomerId = customerId;
            IsLike = isLike;
        }

        public bool IsLike { get; set; }

        public virtual Guid MovieId { get; set; }

       // public virtual Movie Movie { get; set; }

        public virtual Guid CustomerId { get; set; }

       // public virtual Customer Customer { get; set; }
    }
}
