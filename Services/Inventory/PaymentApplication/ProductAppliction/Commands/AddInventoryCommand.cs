using MediatR;

namespace InventoryAppliction.Commands
{
    public record AddInventoryCommand(InventoryModel InventoryModel) : IRequest<InventoryModel>;
}