using MediatR;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Commands
{
    public record InventoryQuantitiesCommand(InventoryQuantities InventoryQuantities) : IRequest<InventoryQuantities>;

}
