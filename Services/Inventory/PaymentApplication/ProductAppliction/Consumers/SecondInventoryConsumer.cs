using InventoryAppliction.Commands;
using MassTransit;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Consumers
{
    public class SecondInventoryConsumer : IConsumer<InventoryQuantities>, GenericInventoryConsumer
    {

        public async Task Consume(ConsumeContext<InventoryQuantities> context)
        {
            Console.WriteLine("Second Inventory Consumer is Consumed");
        }


    }
}