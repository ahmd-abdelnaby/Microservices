using Grpc.Core;
using OrderApplication.Context;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using OrderApplication.Queries;

namespace OrderGrpcService.Services
{
    public class OrderService : OrderGrpcService.OrderServices.OrderServicesBase
    {
        private readonly IMediator _mediatR;

        public OrderService( IMediator mediator)
        {
            _mediatR = mediator;
        }
        public override async Task<Task> GetOrderByOrderId(OrderIdRequest request, IServerStreamWriter<OrderListResponse> responseStream, ServerCallContext context)
        {
            var order = await _mediatR.Send(new GetOrderByIdQuery(request.Id));
            return Task.CompletedTask;
        }
    }
}
