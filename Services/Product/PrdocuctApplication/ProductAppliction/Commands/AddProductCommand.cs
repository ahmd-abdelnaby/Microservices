using MediatR;

namespace ProductAppliction.Commands
{
    public record AddProductCommand(ProductModel productModel) : IRequest<ProductModel>;
}