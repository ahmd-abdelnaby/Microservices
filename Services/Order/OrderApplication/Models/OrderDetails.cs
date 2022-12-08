using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrderApplication.Models
{
    public class OrderDetails
    {
        public int Id { get; set; } 
        public int OderId { get; set; }    
        public int ProductId { get; set; }    
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public virtual Order? Order { get; set; }



    }
}
