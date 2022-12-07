using OrderApplication.Enums;
using OrderApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.ViewModels
{
    public class OrderVM
    {
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }

        public List<OrderDetailsVM> Details { get; set; }

    }
}
