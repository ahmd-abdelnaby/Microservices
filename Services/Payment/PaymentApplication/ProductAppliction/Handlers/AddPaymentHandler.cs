using MediatR;
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
            await _unitOfWork.Repository<Payment>().InsertAsync(
                new Payment
                {
                    Id = request.PaymentModel.Id,
                    Amount = request.PaymentModel.Amount,
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