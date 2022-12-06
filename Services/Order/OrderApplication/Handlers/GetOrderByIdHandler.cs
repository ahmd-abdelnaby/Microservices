using MediatR;
using OrderApplication.Queries;
using Serilog;

namespace OrderApplication.Handlers
{
    internal class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly ILogger _logger;
        public GetOrderByIdHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get order {id}", request.Id);
            return new Order() { TotalPrice = 100, id = request.Id, OrderDate = DateTime.Now };
        }
    }
}
