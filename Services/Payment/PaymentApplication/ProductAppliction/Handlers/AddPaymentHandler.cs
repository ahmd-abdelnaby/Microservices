using Grpc.Core;
using Grpc.Net.Client;
using MediatR;
using OrderGrpcService;
using PaymentAppliction.Commands;
using PaymentAppliction.Events;
using PaymentDomain.Entities;
using PaymentDomain.Interfaces;
using Serilog;

namespace PaymentAppliction.Handlers
{
    public class AddPaymentHandler : IRequestHandler<AddPaymentCommand, PaymentModel>
    {
        private readonly ILogger _logger;
        private readonly IPublisher _publisher;
        private readonly IUnitOfWork<IPaymentContext> _unitOfWork;

        public AddPaymentHandler(ILogger logger, IPublisher publisher, IUnitOfWork<IPaymentContext> unitOfWork)
        {
            _logger = logger;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentModel> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("insert Payment");

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new OrderServices.OrderServicesClient(channel);

            var response = client.GetOrderByOrderId(new OrderGrpcService.OrderIdRequest() { Id = request.PaymentModel.OrderId });

            Order? order  = null;

            while (response.ResponseStream.MoveNext().Result)
            {
                order = response.ResponseStream.Current.Order;
            }

            decimal Amount = decimal.TryParse(order?.TotalPrice.ToString(), out Amount) ? Amount : 0;
            await _unitOfWork.Repository<Payment>().InsertAsync(
                new Payment
                {
                    Id = request.PaymentModel.Id,
                    Amount = Amount,
                    Date = request.PaymentModel.Date,
                    OrderId = request.PaymentModel.OrderId,
                    Status = request.PaymentModel.Status
                });
            await  _unitOfWork.SaveChanges();
            await _publisher.Publish(new PaymentAddedEvent
            {
                Id = request.PaymentModel.Id,
                Amount = request.PaymentModel.Amount,
                Date = request.PaymentModel.Date,
                OrderId = request.PaymentModel.OrderId,
                Status = request.PaymentModel.Status
            }, cancellationToken) ;

            return request.PaymentModel;
        }
    }
}