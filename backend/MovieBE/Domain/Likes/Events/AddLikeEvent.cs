using Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Events
{
    public class AddLikeEvent : Event
    {
        public AddLikeEvent(Guid id, Guid movieId, Guid customerId, bool isLike)
        {
            Id = id;
            CustomerId = customerId;
            IsLike = isLike;
            MovieId = movieId;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsLike { get; set; }
    }
}
