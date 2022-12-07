using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
namespace Product.Infrastrucure
{
    public class ProductContext : DbContext
    {
      
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        public DbSet<Product.Domain.Entities.Product> Products { get; set; }
    }
}