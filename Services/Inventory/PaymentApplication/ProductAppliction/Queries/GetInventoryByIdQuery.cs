using MediatR;
using InventoryAppliction;

namespace InventoryApplication.Queries
{
    public record GetInventoryByIdQuery(int Id) : IRequest<InventoryModel>;
}
