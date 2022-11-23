using MediatR;
using ProductAppliction;

namespace OrderApplication.Queries
{
    public record GetAllProductsQuery() : IRequest<List<ProductModel>>;
}
