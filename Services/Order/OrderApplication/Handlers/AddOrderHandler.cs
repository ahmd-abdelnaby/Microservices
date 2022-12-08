using ApiHelper;
using AutoMapper;
using MassTransit;
using MassTransitConsumer;
using MediatR;
using OrderApplication.Commands;
using OrderApplication.Context;
using OrderApplication.DTO;
using OrderApplication.Enums;
using OrderApplication.Models;
using OrderApplication.ViewModels;
using Serilog;
using SharedMessages;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, bool>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _PublishEndpoint;
        private readonly OrderDBContext _Context;
        private readonly IMapper _mapper;

        public AddOrderHandler(ILogger logger, IPublishEndpoint PublishEndpoint, OrderDBContext Context, IMapper mapper)
        {
            _logger = logger;
            _PublishEndpoint = PublishEndpoint;
            _Context = Context;
            _mapper= mapper;    

        }
        public async Task<bool> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orderProducts = new List<ProductModel>();
                foreach (var item in request.order.Details)
                    orderProducts.Add(new ProductModel
                    {
                        Id = item.ProductId,
                        Qauntity = item.Quantity
                    });
                //call Inventory api
                var api = new ApiClient<List<ProductModel>, List<ProductAvaliblity>>("Inventory/CehckAvalibleProductQuntity", "https://localhost:7120/api/");
                var data = await api.Post(orderProducts);
                if (!(data.Where(x => !x.Avalible).Any()))
                {

                   // var order = _mapper.Map<OrderDto, Order>(request.order);
                    Order order = new Order()
                    {
                        OrderDate = DateTime.Now,
                        PaymentDate = null,
                        Status = OrderStatus.NotPaid,
                        TotalPrice = request.order.TotalPrice,
                    };
                    order.Details = new List<OrderDetails>();
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
                            ProductId = det.ProductId,
                            Quantity = det.Quantity
                        });
                    }
                    await _Context.Orders.AddAsync(order);
                    var result = _Context.SaveChanges();
                    if (result> 0)
                    {

                        await this._PublishEndpoint.Publish<InventoryQuantities>(new InventoryQuantities() { Qts = Qts });

                        _logger.Information("insert order And it is Published To Consumers");
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
