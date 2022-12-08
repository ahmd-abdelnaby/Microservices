using MediatR;
using OrderApplication.DTO;

namespace OrderApplication.Commands
{
    public record AddOrderCommand(OrderDto order) : IRequest<bool>;
}