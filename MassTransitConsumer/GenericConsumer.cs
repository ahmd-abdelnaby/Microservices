using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace MassTransitConsumer
{
    public class GenericConsumer<Tmessage> : IConsumer<Tmessage> where Tmessage : class
    {

        public virtual async Task Consume(ConsumeContext<Tmessage> context)
        {
            await Console.Out.WriteLineAsync("new order added with Cost : " + context.Message);
        }

    }
  
}