using InventoryAppliction.Commands;
using InventoryAppliction.Events;
using MediatR;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Handlers
{
    public class InventoryQuantitiesHandler : IRequestHandler<InventoryQuantitiesCommand, InventoryQuantities>
    {
        private readonly IPublisher _publisher;

        public InventoryQuantitiesHandler( IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<InventoryQuantities> Handle(InventoryQuantitiesCommand request, CancellationToken cancellationToken)
        {

            return request.InventoryQuantities;
        }
    }
}
