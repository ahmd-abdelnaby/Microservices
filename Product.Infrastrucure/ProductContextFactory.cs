using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucure
{
    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseSqlServer("Data Source=197.168.1.248;Initial Catalog=MirageMSProduct;User Id=sa;Password=ExcelSystems@2017;TrustServerCertificate=True;");

            return new ProductContext(optionsBuilder.Options);
        }
    }
}
