using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentDomain.Entities
{
    public class Payment: BaseEntity
    {
		public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
