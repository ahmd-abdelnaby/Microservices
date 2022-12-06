using InventoryDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDomain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(Object id);
        IEnumerable<TEntity> GetAll();
        TEntity Insert(TEntity entity);
        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        Task<TEntity> GetByIdAsync(Object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> InsertAsync(TEntity entity);
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
        IQueryable<TEntity> Table { get; }
    }
}
