using MediatR;
using OrderApplication.Queries;
using Serilog;

namespace OrderApplication.Handlers
{
    internal class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<Order>>
    {
        private readonly ILogger _logger;
        public GetAllOrdersHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get all orders");
            return Enumerable.Range(1, 5).Select(index => new Order
            {
                OrderDate = DateTime.Now.AddDays(index),
                TotalPrice = 100,
                id = 1

            }).ToList();
        }
    }
}
