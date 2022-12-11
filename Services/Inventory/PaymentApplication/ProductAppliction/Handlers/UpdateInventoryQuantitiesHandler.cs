using InventoryAppliction.Commands;
using InventoryAppliction.Events;
using InventoryInfrastructure;
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

        public UpdateInventoryQuantitiesHandler(InventoryContext Context)
        {
            _Context = Context;
        }

        public async Task<bool> Handle(UpdateInventoryQuantitiesCommand request, CancellationToken cancellationToken)
        {
            
                foreach (var Inventory in request.Quantities.ProductQuantities)
                {
                    var OldInventory = _Context.Inventorys.FirstOrDefault(x => x.ProductId == Inventory.ProductId);

                    if (OldInventory != null)
                    {
                        if (OldInventory.Qauntity >= Inventory.Quantity)
                        {
                            OldInventory.Qauntity -= Inventory.Quantity;
                            _Context.Inventorys.Update(OldInventory);
                        }
                    }
                }
               var result=  _Context.SaveChanges();

              return (result > 0)? true : false;
            
        }
    }
}