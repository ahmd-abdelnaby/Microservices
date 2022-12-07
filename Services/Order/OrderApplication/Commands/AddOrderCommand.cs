using MediatR;
using OrderApplication.ViewModels;

namespace OrderApplication.Commands
{
    public record AddOrderCommand(OrderVM order) : IRequest<bool>;
}