using MassTransit;
using MassTransitHelper;
using ProductAppliction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAppliction.Consumers
{
    public class OrderConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
    {
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await Console.Out.WriteLineAsync("new order added with Cost : " + context.Message.ToString());
        }
    }
}