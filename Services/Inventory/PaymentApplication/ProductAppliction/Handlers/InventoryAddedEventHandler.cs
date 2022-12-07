using MediatR;
using Microsoft.Extensions.Logging;
using InventoryAppliction.Events;

namespace InventoryAppliction.Handlers
{
    public class InventoryAddedEventHandler : NotificationHandler<InventoryAddedEvent>
    {
        private readonly ILogger _logger;
        public InventoryAddedEventHandler(ILogger<InventoryAddedEventHandler> logger)
        {
            _logger = logger;
        }

        protected override void Handle(InventoryAddedEvent notification)
        {
            _logger.Log(LogLevel.Information, "InventoryAddedEvent");
        }
    }
}
