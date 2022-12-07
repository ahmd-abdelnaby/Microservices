using MediatR;
using InventoryAppliction;
using InventoryAppliction.Models;

namespace InventoryApplication.Queries
{
    public record CheckAvalibleProductQuntityQuery(List<ProductModel> Products) : IRequest<List<ProductAvaliblity>>;
}
