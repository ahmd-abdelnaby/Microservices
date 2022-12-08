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

            var orderProducts = _mapper.Map<List<OrderDetailsDto>, List<ProductModel>>(request.order.Details);
             
            //call Inventory api
              var api = new ApiClient<List<ProductModel>, List<ProductAvaliblity>>("Inventory/CehckAvalibleProductQuntity", "https://localhost:7121/api/");
              var data = await api.Post(orderProducts);
              if (!(data.Where(x => !x.Avalible).Any()))
                {

                    var order = _mapper.Map<OrderDto, Order>(request.order);
                 
                    var ProductQuantities = _mapper.Map<List<OrderDetailsDto>,List<ProductQuantities>>
                        (request.order.Details);

       
                    await _Context.Orders.AddAsync(order);
                    var result = _Context.SaveChanges();
                    if (result> 0)
                    {

                        await this._PublishEndpoint.Publish<InventoryQuantities>(new InventoryQuantities() { ProductQuantities = ProductQuantities });

                        _logger.Information("insert order And it is Published To Consumers");
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
           
        }
    }
}
