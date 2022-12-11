using MediatR;
using InventoryApplication.Queries;
using InventoryDomain.Entities;
using InventoryDomain.Interfaces;
using Serilog;
using InventoryAppliction.Models;

namespace InventoryAppliction.Handlers
{
    internal class CheckAvalibleProductQuntityHandler : IRequestHandler<CheckAvalibleProductQuntityQuery, List<ProductAvaliblity> >
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork<IInventoryContext> _unitOfWork;

        public CheckAvalibleProductQuntityHandler(IUnitOfWork<IInventoryContext> unitOfWork, ILogger logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public async Task<List<ProductAvaliblity>> Handle(CheckAvalibleProductQuntityQuery request, CancellationToken cancellationToken)
        {
            List<ProductAvaliblity> list = new List<ProductAvaliblity>();
            foreach (var requestItem in request.Products)
            {
                var item = _unitOfWork.Repository<Inventory>().Table.Where(x=>x.ProductId== requestItem.Id).FirstOrDefault();
                if (item != null)
                {
                    list.Add(new ProductAvaliblity
                    {
                        Id = requestItem.Id,
                        Avalible = item.Quantity > 0 && requestItem.Quantity <= item.Quantity,
                        Reason = ""
                    });
                }
                else
                {
                    list.Add(new ProductAvaliblity
                    {
                        Id = requestItem.Id,
                        Avalible = false,
                        Reason = "wrong product Id"
                    });
                }
            }
            return list;
        }
    }
}
