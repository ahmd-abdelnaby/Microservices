using MassTransit;
using MassTransitConsumer;
using ProductAppliction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAppliction.Consumers
{
    public class EmailConsumer:/*IConsumer<ProductOrderModel> */ GenericConsumer<ProductOrderModel>
    {

        public override async Task Consume(ConsumeContext<ProductOrderModel> context)
        {
            await Console.Out.WriteLineAsync("new order added with Cost : " + context.Message.ToString());
        }
    }
}