using MassTransit;
using MassTransitHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAppliction.Consumers
{
    public class PaymentConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
    {
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await Console.Out.WriteLineAsync("new order added with Cost : " + context.Message.ToString());
        }
    }
}