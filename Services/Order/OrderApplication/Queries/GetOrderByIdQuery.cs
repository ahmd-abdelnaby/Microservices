using MediatR;

namespace OrderApplication.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<OrderModel>;
}
