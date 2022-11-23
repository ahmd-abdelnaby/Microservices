using MediatR;
using OrderApplication;

namespace OrderApplication.Queries
{
    public record GetAllOrdersQuery() : IRequest<List<OrderModel>>;
}
