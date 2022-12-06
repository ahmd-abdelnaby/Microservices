using MediatR;

namespace OrderApplication.Commands
{
    public record AddOrderCommand(Order orderModel) : IRequest<Order>;
}