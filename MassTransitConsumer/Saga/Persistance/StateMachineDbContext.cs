using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumer.Saga.Persistance
{
    public class StateMachineDbContext : DbContext
    {
        public StateMachineDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<OrderState> BusinessObjectStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderStateConfiguration());
        }
    }
}
