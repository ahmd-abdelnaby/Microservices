using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAppliction.Models
{

    public class PaymentOrderModel
    {
        public int id { get; set; }
        public decimal? cost { get; set; }
        public DateTime Date { get; set; }

    }
}