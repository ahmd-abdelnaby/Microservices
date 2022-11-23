using MediatR;
using OrderApplication.Queries;
using Serilog;

namespace OrderApplication.Handlers
{
    internal class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderModel>>
    {
        private readonly ILogger _logger;
        public GetAllOrdersHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<List<OrderModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get all orders");
            return Enumerable.Range(1, 5).Select(index => new OrderModel
            {
                Date = DateTime.Now.AddDays(index),
                cost = 100,
                id = 1

            }).ToList();
        }
    }
}
