using MassTransit;
using MassTransit.Mediator;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.Consumers
{

    public class InventoryQuantitiesFailedConsumer : IConsumer<InventoryQuantitiesFailed>, GenericConsumer
    {
        


        public InventoryQuantitiesFailedConsumer()
        {
            
        }

        public Task Consume(ConsumeContext<InventoryQuantitiesFailed> context)
        {
            throw new NotImplementedException();
        }
    }
}