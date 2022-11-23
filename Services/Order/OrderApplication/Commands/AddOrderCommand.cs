using MediatR;
using OrderApplication;

namespace OrderApplication.Commands
{
    public record AddOrderCommand(OrderModel orderModel) : IRequest<OrderModel>;
}