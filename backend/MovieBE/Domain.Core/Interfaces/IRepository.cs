using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
    where TEntity : class
    {
        void Add(TEntity obj);

        TEntity GetById(Guid id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(ISpecification<TEntity> spec);

        IQueryable<TEntity> GetAllSoftDeleted();

        void Update(TEntity obj);

        void Remove(Guid id);

        int SaveChanges();
    }
}
