using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace MassTransitConsumer.Consumers.Order
{
    public class OrderConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
    {

        public virtual async Task Consume(ConsumeContext<TMessage> context)
        {
            await Console.Out.WriteLineAsync("new order added with Cost : " + context.Message);
        }

    }

}