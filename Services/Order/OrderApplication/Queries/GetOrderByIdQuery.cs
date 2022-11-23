using MediatR;
using OrderApplication;

namespace OrderApplication.Queries
{
    public record GetOrderByIdQuery(int Id) : IRequest<OrderModel>;
}
