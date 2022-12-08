using OrderApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.DTO
{
    public class OrderDto
    {
        public double TotalPrice { get; set; }
        public List<OrderDetailsDto> Details { get; set; }

    }
}
