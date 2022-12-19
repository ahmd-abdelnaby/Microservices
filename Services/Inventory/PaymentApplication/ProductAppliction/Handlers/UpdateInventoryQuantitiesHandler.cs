using InventoryAppliction.Commands;
using InventoryAppliction.Events;
using InventoryInfrastructure;
using MassTransit;
using MediatR;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Handlers
{
    public class UpdateInventoryQuantitiesHandler : IRequestHandler<UpdateInventoryQuantitiesCommand, bool>
    {

        private readonly InventoryContext _Context;
        private readonly IPublishEndpoint _PublishEndpoint;

        public UpdateInventoryQuantitiesHandler(InventoryContext Context, IPublishEndpoint PublishEndpoint)
        {
            _Context = Context;
            _PublishEndpoint = PublishEndpoint;
        }

        public async Task<bool> Handle(UpdateInventoryQuantitiesCommand request, CancellationToken cancellationToken)
        {
            foreach (var Inventory in request.Quantities.ProductQuantities)
            {
                var OldInventory = _Context.Inventorys.FirstOrDefault(x => x.ProductId == Inventory.ProductId);

                if (OldInventory != null)
                {
                    if (OldInventory.Quantity >= Inventory.Quantity)
                    {
                        OldInventory.Quantity -= Inventory.Quantity;
                        _Context.Inventorys.Update(OldInventory);
                    }
                }
            }
            var result=  _Context.SaveChanges();

            if (result > 0)
            {
                
                return true;
            }
            else
            {
                await _PublishEndpoint.Publish(new InventoryQuantitiesFailed
                {
                    OrderId = Guid.NewGuid(),
                    ProductQuantities = request.Quantities.ProductQuantities
                });
                return false;
            }
        }
    }
}