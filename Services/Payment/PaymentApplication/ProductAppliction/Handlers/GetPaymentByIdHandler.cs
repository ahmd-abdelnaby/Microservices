using MediatR;
using PaymentApplication.Queries;
using Serilog;

namespace PaymentAppliction.Handlers
{
    internal class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, PaymentModel>
    {
        private readonly ILogger _logger;
        public GetPaymentByIdHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<PaymentModel> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get Payment {id}", request.Id);
            return new PaymentModel()
            {
            };
        }
    }
}
