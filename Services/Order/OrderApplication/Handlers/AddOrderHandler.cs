using ApiHelper;
using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using InventoryGrpcService;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using OrderApplication.Commands;
using OrderApplication.Context;
using OrderApplication.DTO;
using OrderApplication.Enums;
using OrderApplication.Models;
using OrderApplication.Settings;
using Serilog;
using SharedMessages;
using ProductModel = OrderApplication.DTO.ProductModel;

namespace OrderApplication.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrderCommand, bool>
    {
        private readonly ILogger _logger;
        private readonly IPublishEndpoint _PublishEndpoint;
        private readonly OrderDBContext _Context;
        private readonly IMapper _mapper;
        private readonly InventorySettings _inventorySettings;
        public AddOrderHandler(ILogger logger, IPublishEndpoint PublishEndpoint, OrderDBContext Context, IMapper mapper, IOptions<InventorySettings> inventorySettings)
        {
            _logger = logger;
            _PublishEndpoint = PublishEndpoint;
            _Context = Context;
            _mapper= mapper;
            _inventorySettings = inventorySettings.Value;

        }
        public async Task<bool> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var orderProducts = _mapper.Map<List<OrderDetailsDto>, List<ProductModel>>(request.order.Details);

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new InventoryGrpcService.InventoryServices.InventoryServicesClient(channel);

            ProductModelRequest productModelRequest = new ProductModelRequest();
            foreach (var order in orderProducts)
                productModelRequest.ProductModel.Add(new InventoryGrpcService.ProductModel { ProductId = order.Id, Quantity = order.Quantity });


            var response = client.CheckAvalibleProductQuntity(productModelRequest);


            var productAvaliblity = _mapper.Map<RepeatedField<ProductAvaliblityResponseModel>,
                List<ProductAvaliblity>>(response.ProductAvaliblityResponseModels);

            if (!(productAvaliblity.Where(x => !x.Avalible).Any()))
            {

                var order = _mapper.Map<OrderDto, Order>(request.order);

                var ProductQuantities = _mapper.Map<List<OrderDetailsDto>, List<ProductQuantities>>
                    (request.order.Details);


                await _Context.Orders.AddAsync(order);
                var result = _Context.SaveChanges();
                if (result > 0)
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