using InventoryAppliction.Commands;
using MassTransit;
using MassTransitHelper;
using MediatR;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Consumers
{
    public class SecondConsumer : IConsumer<OrderPaymentMEssage>, GenericInventoryConsumer
    {
        private readonly IMediator _mediator;


        public SecondConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderPaymentMEssage> context)
        {
            Console.WriteLine(context.Message.TotalPrice);
        }


    }
}