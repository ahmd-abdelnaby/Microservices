using MediatR;
using PaymentAppliction;

namespace PaymentApplication.Queries
{
    public record GetPaymentByIdQuery(int Id) : IRequest<PaymentModel>;
}
