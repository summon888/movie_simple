using Domain.Core.Interfaces;
using Domain.Likes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Likes.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        Like GetLike(Guid movieId, Guid customerId, bool isLike);

        List<Like> GetLike(Guid movieId, bool isLike);

        List<Like> GetLike(List<Guid> movieIds);
    }
}
