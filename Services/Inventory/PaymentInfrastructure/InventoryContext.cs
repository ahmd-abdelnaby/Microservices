using Microsoft.EntityFrameworkCore;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;

namespace InventoryInfrastructure
{
    public class InventoryContext : DbContext, IInventoryContext
    {

        public InventoryContext(DbContextOptions<InventoryContext> options): base(options)
        {
        }
        public virtual DbSet<Inventory> Inventorys { get; set; }
    }
        
    }
