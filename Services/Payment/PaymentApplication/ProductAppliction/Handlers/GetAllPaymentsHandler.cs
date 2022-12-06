using MediatR;
using PaymentApplication.Queries;
using PaymentDomain.Entities;
using PaymentDomain.Interfaces;
using Serilog;

namespace PaymentAppliction.Handlers
{
    internal class GetAllPaymentsHandler : IRequestHandler<GetAllPaymentsQuery, List<PaymentModel>>
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork<IPaymentContext> _unitOfWork;

        public GetAllPaymentsHandler(IUnitOfWork<IPaymentContext> unitOfWork, ILogger logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public async Task<List<PaymentModel>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get all Payments");
            List<PaymentModel> list = new List<PaymentModel>();
            var data = _unitOfWork.Repository<Payment>().Table.ToList();
            foreach (var item in data)
                list.Add(new PaymentModel
                {
                    id = item.id,
                    Amount = item.Amount,
                    Date = item.Date,
                    OrderId=item.OrderId,
                    Status=item.Status
                });
            return list;
        }
    }
}
