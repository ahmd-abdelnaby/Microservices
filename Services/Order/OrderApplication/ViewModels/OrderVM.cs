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
        public double TotalPrice { get; set; }
        public List<OrderDetailsVM> Details { get; set; }

    }
}
