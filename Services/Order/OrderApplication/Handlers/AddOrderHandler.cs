using MassTransit;
using MassTransitConsumer;
using MassTransitConsumer.Messages.Order;
using MediatR;
using OrderApplication.Commands;
using Serilog;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, OrderModel>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _PublishEndpoint;

        public AddOrderHandler(ILogger logger, IPublishEndpoint PublishEndpoint)
        {
            _logger = logger;
            _PublishEndpoint = PublishEndpoint;

        }
        public async Task<OrderModel> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            await this._PublishEndpoint.Publish<OrderMessage>(new OrderMessage {cost= request.orderModel.cost });

            _logger.Information("insert order And it is Published To Consumers");
            return request.orderModel;
        }
    }
}
