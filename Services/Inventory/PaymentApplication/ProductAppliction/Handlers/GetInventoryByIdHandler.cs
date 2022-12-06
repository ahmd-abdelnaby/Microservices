using MediatR;
using InventoryApplication.Queries;
using Serilog;

namespace InventoryAppliction.Handlers
{
    internal class GetInventoryByIdHandler : IRequestHandler<GetInventoryByIdQuery, InventoryModel>
    {
        private readonly ILogger _logger;
        public GetInventoryByIdHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<InventoryModel> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get Inventory {id}", request.Id);
            return new InventoryModel()
            {
            };
        }
    }
}
