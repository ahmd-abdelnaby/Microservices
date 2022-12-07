using MediatR;

namespace PaymentAppliction.Commands
{
    public record AddPaymentCommand(PaymentModel PaymentModel) : IRequest<PaymentModel>;
}