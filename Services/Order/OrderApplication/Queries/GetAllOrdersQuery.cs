using MediatR;

namespace OrderApplication.Queries
{
    public record GetAllOrdersQuery() : IRequest<List<OrderModel>>;
}
