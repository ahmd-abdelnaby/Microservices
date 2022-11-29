using MediatR;

namespace OrderApplication.Commands
{
    public record AddOrderCommand(OrderModel orderModel) : IRequest<OrderModel>;
}