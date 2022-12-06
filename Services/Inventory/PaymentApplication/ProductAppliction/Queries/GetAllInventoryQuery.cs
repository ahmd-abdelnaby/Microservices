using MediatR;
using InventoryAppliction;

namespace InventoryApplication.Queries
{
    public record GetAllInventorysQuery() : IRequest<List<InventoryModel>>;
}
