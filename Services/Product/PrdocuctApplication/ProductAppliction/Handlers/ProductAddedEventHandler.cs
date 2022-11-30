using MediatR;
using Microsoft.Extensions.Logging;
using ProductAppliction.Events;

namespace ProductAppliction.Handlers
{
    public class ProductAddedEventHandler : NotificationHandler<ProductAddedEvent>
    {
        private readonly ILogger _logger;
        public ProductAddedEventHandler(ILogger<ProductAddedEventHandler> logger)
        {
            _logger = logger;
        }

        protected override void Handle(ProductAddedEvent notification)
        {
            _logger.Log(LogLevel.Information, "ProductAddedEvent");
        }
    }
}
