using MediatR;
using OrderApplication.DTO;

namespace OrderApplication.Commands
{
    public record AddOrderCommand(Order orderModel) : IRequest<bool>;
}