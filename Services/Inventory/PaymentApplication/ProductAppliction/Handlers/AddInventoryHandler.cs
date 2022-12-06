using MediatR;
using InventoryAppliction.Commands;
using InventoryAppliction.Events;
using Serilog;

namespace InventoryAppliction.Handlers
{
    public class AddInventoryHandler : IRequestHandler<AddInventoryCommand, InventoryModel>
    {
        private readonly ILogger _logger;
        private readonly IPublisher _publisher;

        public AddInventoryHandler(ILogger logger, IPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<InventoryModel> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("insert Inventory");

            await _publisher.Publish(new InventoryAddedEvent
            {
                 ProductId = request.InventoryModel.ProductId,
                 Qauntity = request.InventoryModel.Qauntity
            }, cancellationToken) ;

            return request.InventoryModel;
        }
    }
}