using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumer.Messages.Order
{
    public class OrderMessage
    {
        public decimal? cost { get; set; }
    }
}
