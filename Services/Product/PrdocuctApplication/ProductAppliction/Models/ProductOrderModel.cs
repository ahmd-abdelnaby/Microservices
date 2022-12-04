using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAppliction.Models
{

    public class ProductOrderModel
    {
        public int id { get; set; }
        public decimal? cost { get; set; }
        public DateTime Date { get; set; }

    }
}