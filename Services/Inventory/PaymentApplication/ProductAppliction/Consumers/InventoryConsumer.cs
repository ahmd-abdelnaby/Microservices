using InventoryApplication.Queries;
using InventoryAppliction.Commands;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using InventoryInfrastructure;
using MassTransit;
using MassTransit.SagaStateMachine;
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
    public class InventoryConsumer : IConsumer<InventoryQuantities>, GenericConsumer
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _PublishEndpoint;

        public InventoryConsumer(IMediator mediator, IPublishEndpoint PublishEndpoint)
        {
            _mediator = mediator;
            _PublishEndpoint = PublishEndpoint;
        }

        public async Task Consume(ConsumeContext<InventoryQuantities> context)
        {
             await _mediator.Send( new UpdateInventoryQuantitiesCommand(context.Message));
        }


    }
}