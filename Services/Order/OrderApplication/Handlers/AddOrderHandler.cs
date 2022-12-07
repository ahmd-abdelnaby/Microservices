using MassTransit;
using MassTransitConsumer;
using MediatR;
using OrderApplication.Commands;
using OrderApplication.Context;
using OrderApplication.Models;
using OrderApplication.ViewModels;
using ProductOrderMessage;
using Serilog;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, OrderVM>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _PublishEndpoint;
        private readonly OrderDBContext _Context;
        public AddOrderHandler(ILogger logger, IPublishEndpoint PublishEndpoint, OrderDBContext Context)
        {
            _logger = logger;
            _PublishEndpoint = PublishEndpoint;
            _Context = Context;

        }
        public async Task<OrderVM> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Order order = new Order()
                {
                    OrderDate = request.order.OrderDate,
                    PaymentDate = request.order.PaymentDate,    
                    Status= request.order.Status,   
                    TotalPrice = request.order.TotalPrice,
                };
                order.Details=new List<OrderDetails>();
                foreach (var det in request.order.Details)
                {
                    order.Details.Add(new OrderDetails()
                    {
                        ProductId = det.ProductId,
                        Quantity = det.Quantity,
                        TotalPrice = det.TotalPrice
                    });
                }

                await _Context.Orders.AddAsync(order);
                var result = _Context.SaveChanges();

                await this._PublishEndpoint.Publish<ProductOrderMessageModel>(new ProductOrderMessageModel { Cost = (decimal)request.order.TotalPrice });

                _logger.Information("insert order And it is Published To Consumers");
                return request.order;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
