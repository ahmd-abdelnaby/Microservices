using MediatR;
using InventoryApplication.Queries;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using Serilog;

namespace InventoryAppliction.Handlers
{
    internal class GetAllInventorysHandler : IRequestHandler<GetAllInventorysQuery, List<InventoryModel>>
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork<IInventoryContext> _unitOfWork;

        public GetAllInventorysHandler(IUnitOfWork<IInventoryContext> unitOfWork, ILogger logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public async Task<List<InventoryModel>> Handle(GetAllInventorysQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get all Inventorys");
            List<InventoryModel> list = new List<InventoryModel>();
            var data = _unitOfWork.Repository<Inventory>().Table.ToList();
            foreach (var item in data)
                list.Add(new InventoryModel
                {
                    ProductId = item.ProductId,
                    Qauntity = item.Qauntity,
                });
            return list;
        }
    }
}
