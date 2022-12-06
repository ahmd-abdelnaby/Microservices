using MassTransit;
using MassTransitConsumer;
using MediatR;
using OrderApplication.Commands;
using ProductOrderMessage;
using Serilog;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, Order>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _PublishEndpoint;

        public AddOrderHandler(ILogger logger, IPublishEndpoint PublishEndpoint)
        {
            _logger = logger;
            _PublishEndpoint = PublishEndpoint;

        }
        public async Task<Order> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            await this._PublishEndpoint.Publish<ProductOrderMessageModel>(new ProductOrderMessageModel { Cost= (decimal)request.orderModel.TotalPrice });

            _logger.Information("insert order And it is Published To Consumers");
            return request.orderModel;
        }
    }
}
