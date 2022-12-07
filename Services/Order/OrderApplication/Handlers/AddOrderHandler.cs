using MassTransit;
using MassTransitConsumer;
using MediatR;
using OrderApplication.Commands;
using OrderApplication.Context;
using OrderApplication.Models;
using OrderApplication.ViewModels;
using Serilog;
using SharedMessages;

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
                var Qts = new List<ProductQuantities>();

                foreach (var det in request.order.Details)
                {
                    order.Details.Add(new OrderDetails()
                    {
                        ProductId = det.ProductId,
                        Quantity = det.Quantity,
                        TotalPrice = det.TotalPrice
                    });
                    Qts.Add(new ProductQuantities()
                    {
                         ProductId= det.ProductId,
                         Quantity =det.Quantity
                    });
                }

                await _Context.Orders.AddAsync(order);
                var result = _Context.SaveChanges();
                if (result == 4)
                {

                    await this._PublishEndpoint.Publish<InventoryQuantities>(new InventoryQuantities() { Qts= Qts });

                    _logger.Information("insert order And it is Published To Consumers");
                    return request.order;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
