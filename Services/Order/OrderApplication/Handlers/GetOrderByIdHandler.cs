using MediatR;
using OrderApplication.Queries;
using Serilog;

namespace OrderApplication.Handlers
{
    internal class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderModel>
    {
        private readonly ILogger _logger;
        public GetOrderByIdHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<OrderModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get order {id}", request.Id);
            return new OrderModel() { cost = 100, id = request.Id, Date = DateTime.Now };
        }
    }
}
