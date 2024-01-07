using Domain.Likes.Entities;
using Domain.Likes.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Likes
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public Like GetLike(Guid movieId, Guid customerId, bool isLike) 
        {
            var da = _dbSet.ToList();
            return _dbSet.AsNoTracking().FirstOrDefault(
                a => a.MovieId == movieId
                && a.CustomerId == customerId
                && a.IsLike == isLike);
        }

        public List<Like> GetLike(Guid movieId, bool isLike)
        {
            var da = _dbSet.ToList();
            return _dbSet.AsNoTracking().Where(
                a => a.MovieId == movieId
                && a.IsLike == isLike).ToList();
        }

        public List<Like> GetLike(List<Guid> movieIds)
        {
            if (movieIds == null || !movieIds.Any())
            {
                // Trả về danh sách rỗng nếu movieIds là null hoặc không có phần tử
                return new List<Like>();
            }

            var movieIdsString = string.Join(",", movieIds);
            var result = _dbSet.AsNoTracking()
                .Where(a => movieIdsString.Contains(a.MovieId.ToString()))
                .ToList();

            return result;
        }
    }
}
