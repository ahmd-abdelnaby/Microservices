using OrderApplication.Enums;
using OrderApplication.Models;
using Spectre.Console;

namespace OrderApplication
{
    public class Order
    {
        public int id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual ICollection<OrderDetails>? Details { get; set; }


    }
}