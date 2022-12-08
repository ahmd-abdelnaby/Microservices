using InventoryAppliction.Commands;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using InventoryInfrastructure;
using MassTransit;
using MassTransit.SagaStateMachine;
using MassTransitConsumer;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Consumers
{
    public class InventoryConsumer : IConsumer<InventoryQuantities>
    {
        private readonly InventoryContext _Context;

       
        public InventoryConsumer(InventoryContext Context)
        { 
            _Context=Context;   
        }

        public async Task Consume(ConsumeContext<InventoryQuantities> context)
        {
            
                if (context.Message != null)
                {
                  InventoryQuantities newInventory = context.Message ;
                foreach (var Inventory in newInventory.ProductQuantities)
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
                _Context.SaveChanges();
            }


        }

        
    }
}