using Grpc.Core;
using PaymentDomain.Interfaces;
using System.Linq;

namespace PaymentGrpcService.Services
{
    public class PaymentService : PaymentGrpcService.PaymentServices.PaymentServicesBase
    {
        private readonly IUnitOfWork<IPaymentContext> _unitOfWork;

        public PaymentService(IUnitOfWork<IPaymentContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override Task PaymentDetailsByOrderId(OrderIdRequest request, IServerStreamWriter<PaymentDetailsResponse> responseStream, ServerCallContext context)
        {

            var payments = _unitOfWork.Repository<PaymentDomain.Entities.Payment>().Table.Where(o => o.OrderId == request.Id).ToList();

            foreach (var payment in payments)
            {
                responseStream.WriteAsync(new PaymentDetailsResponse()
                {
                    Payment = new Payment
                    {
                        Id = payment.Id,
                        Amount = double.Parse(payment.Amount.ToString()),
                        Date = payment.Date.ToString(),
                        OrderId = payment.OrderId,
                        Status = payment.Status
                    }
                });
            }
            return Task.CompletedTask;
        }
    }
}
