using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dukkantek.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllQueryable();


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);

        TEntity Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(List<TEntity> entities);
        Task AddRangeAsync(List<TEntity> entities);

        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(int id);
        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate = null);
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void SaveEntities(CancellationToken cancellationToken = default(CancellationToken));
    }
}

