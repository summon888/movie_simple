using Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Commands
{
    public abstract class LikeCommand : Command
    {
        public Guid CustomerId { get; protected set; }
        public Guid MovieId { get; protected set; }
        public bool IsLike { get; protected set; }
    }
}
