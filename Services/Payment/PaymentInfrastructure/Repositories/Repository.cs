using ETA_AdminPanel.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using PaymentDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentInfrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private DbSet<TEntity> entities;
        private DbContext _dbContext;
        public IQueryable<TEntity> Table => this.entities;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<TEntity>();
        }
        public void Delete(TEntity entity)
        {
            this.entities.Remove(entity);
        }
        public void Delete(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
        }
        public TEntity Insert(TEntity entity)
        {
            this.entities.Add(entity);
            return entity;
        }
        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
            return entities;
        }
        public TEntity Update(TEntity entity)
        {
            this.entities.Update(entity);
            return entity;
        }

        public TEntity GetById(object id)
        {
            return this.entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.entities;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.entities.ToListAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await this.entities.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entities)
        {
            await this.entities.AddRangeAsync(entities);
            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.FromResult(this.entities.Update(entity));
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(this.entities.Remove(entity));
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
                await Task.FromResult(this.entities.Remove(item));
        }

    }

}
