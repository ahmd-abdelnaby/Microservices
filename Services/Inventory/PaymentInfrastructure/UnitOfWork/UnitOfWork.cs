using Microsoft.EntityFrameworkCore;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using InventoryInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryInfrastructure.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    {
        private readonly DbContext Context;
        public Dictionary<Type, object> Repositories;

        public UnitOfWork(IServiceProvider services)
        {
            this.Context = services.GetService(typeof(TContext)) as DbContext;

            this.Repositories = new Dictionary<Type, object>();
        }
        public void Dispose()
        {
            this.Context.Dispose();
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (!Repositories.ContainsKey(typeof(TEntity)))
            {
                var repositoryObject = Activator.CreateInstance(typeof(Repository<TEntity>), this.Context);
                this.Repositories.Add(typeof(TEntity), repositoryObject);
            }
            return this.Repositories[typeof(TEntity)] as IRepository<TEntity>;
        }
        public async Task<int> SaveChanges()
        {
            return await this.Context.SaveChangesAsync();
        }
    }

}
