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
                  InventoryQuantities newInv = context.Message as InventoryQuantities;
                  var x=  new InventoryQuantitiesCommand(newInv);
                foreach (var Inv in newInv.Qts)
                {
                    var OldInv = _Context.Inventorys.FirstOrDefault(x => x.ProductId == Inv.ProductId);

                    if (OldInv != null)
                    {
                        if (OldInv.Qauntity >= Inv.Quantity)
                        {
                            OldInv.Qauntity -= Inv.Quantity;
                            _Context.Inventorys.Update(OldInv);
                            _Context.SaveChanges();
                        }
                    }
                }
                }


        }

        
    }
}