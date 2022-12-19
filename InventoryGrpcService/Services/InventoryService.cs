using AutoMapper;
using Grpc.Core;
using InventoryApplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryGrpcService.Services
{
    public class InventoryService : InventoryGrpcService.InventoryServices.InventoryServicesBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public InventoryService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override Task<ProductAvaliblityResponse> CheckAvalibleProductQuntity(ProductModelRequest request, ServerCallContext context)
        {
            var productsAvailable = _mediator.Send(new CheckAvalibleProductQuntityQuery
                                (_mapper.Map<List<InventoryGrpcService.ProductModel>,
                   List<InventoryAppliction.Models.ProductModel>>(request.ProductModel.ToList())));


            ProductAvaliblityResponse productAvaliblityResponse = new ProductAvaliblityResponse();

            foreach (var product in productsAvailable.Result)
            {
                productAvaliblityResponse.ProductAvaliblityResponseModels.Add(
                    new ProductAvaliblityResponseModel()
                    {
                        Avalible = product.Avalible,
                        Id = product.Id,
                        Reason = product.Reason
                    });
            }

            return Task.FromResult(productAvaliblityResponse);
        }
    }
}