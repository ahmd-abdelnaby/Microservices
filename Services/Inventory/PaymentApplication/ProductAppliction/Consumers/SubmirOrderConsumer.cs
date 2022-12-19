using InventoryApplication.Queries;
using InventoryAppliction.Commands;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using InventoryInfrastructure;
using MassTransit;
using MassTransit.SagaStateMachine;
using MediatR;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>, GenericInventoryConsumer
    {
        private readonly IMediator _mediator;


        public SubmitOrderConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
             //await _mediator.Send( new UpdateInventoryQuantitiesCommand(context.Message));
        }


    }
}