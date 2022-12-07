using MassTransit;
using MassTransitConsumer;
using MediatR;
using OrderApplication.Commands;
using ProductOrderMessage;
using Serilog;
using OrderApplication.DTO;
using ApiHelper;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, bool>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _PublishEndpoint;

        public AddOrderHandler(ILogger logger, IPublishEndpoint PublishEndpoint)
        {
            _logger = logger;
            _PublishEndpoint = PublishEndpoint;

        }
        public async Task<bool> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            //await this._PublishEndpoint.Publish<ProductOrderMessageModel>(new ProductOrderMessageModel { Cost= (decimal)request.orderModel.TotalPrice });
            var orderProducts = new List<ProductModel>();
            foreach (var item in request.orderModel.Details)
                orderProducts.Add(new ProductModel
                { 
                  Id = item.ProductId,
                  Qauntity = item.Quantity
                });
            //call Inventory api
            var api = new ApiClient<List<ProductModel>,List<ProductAvaliblity>>("Inventory/CehckAvalibleProductQuntity", "https://localhost:7120/api/");
            var data= await  api.Post(orderProducts);
            return !(data.Where(x => !x.Avalible).Any());
        }
    }
}
