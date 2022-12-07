using MediatR;
using Microsoft.Extensions.Logging;
using PaymentAppliction.Events;

namespace PaymentAppliction.Handlers
{
    public class PaymentAddedEventHandler : NotificationHandler<PaymentAddedEvent>
    {
        private readonly ILogger _logger;
        public PaymentAddedEventHandler(ILogger<PaymentAddedEventHandler> logger)
        {
            _logger = logger;
        }

        protected override void Handle(PaymentAddedEvent notification)
        {
            _logger.Log(LogLevel.Information, "PaymentAddedEvent");
        }
    }
}
