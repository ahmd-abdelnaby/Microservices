using MediatR;
using PaymentAppliction;

namespace PaymentApplication.Queries
{
    public record GetAllPaymentsQuery() : IRequest<List<PaymentModel>>;
}
