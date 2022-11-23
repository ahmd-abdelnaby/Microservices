using MediatR;
using ProductAppliction;

namespace ProductApplication.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductModel>;
}
