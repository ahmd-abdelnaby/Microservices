using MediatR;
using PaymentAppliction.Commands;
using PaymentAppliction.Events;
using Serilog;

namespace PaymentAppliction.Handlers
{
    public class AddPaymentHandler : IRequestHandler<AddPaymentCommand, PaymentModel>
    {
        private readonly ILogger _logger;
        private readonly IPublisher _publisher;

        public AddPaymentHandler(ILogger logger, IPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<PaymentModel> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("insert Payment");

            await _publisher.Publish(new PaymentAddedEvent
            {
                Id = request.PaymentModel.id,
                Amount = request.PaymentModel.Amount,
                Date = request.PaymentModel.Date,
                OrderId = request.PaymentModel.OrderId,
                Status = request.PaymentModel.Status
            }, cancellationToken) ;

            return request.PaymentModel;
        }
    }
}