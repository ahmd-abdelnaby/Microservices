using InventoryApplication.Queries;
using InventoryAppliction.Commands;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using InventoryInfrastructure;
using MassTransit;
using MassTransit.SagaStateMachine;
using MassTransitConsumer;
using MediatR;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Consumers
{
    public class InventoryConsumer : IConsumer<InventoryQuantities>, GenericInventoryConsumer
    {
        private readonly IMediator _mediator;


        public InventoryConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<InventoryQuantities> context)
        {
             await _mediator.Send( new UpdateInventoryQuantitiesCommand(context.Message));
        }


    }
}