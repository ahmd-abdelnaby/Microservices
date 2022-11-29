using MediatR;
using OrderApplication.Commands;
using Serilog;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, OrderModel>
    {
        private readonly ILogger _logger;

        public AddOrderHandler(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<OrderModel> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("insert order");
            return request.orderModel;
        }
    }
}
