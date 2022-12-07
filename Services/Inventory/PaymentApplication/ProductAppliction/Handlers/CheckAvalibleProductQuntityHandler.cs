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
            var data = _unitOfWork.Repository<Inventory>().Table.ToList();
            foreach (var item in data)
            {
                var requestedItem = request.Products.Where(x => x.Id== item.ProductId).FirstOrDefault();
                if (requestedItem!=null)
                {

                    list.Add(new ProductAvaliblity
                    {
                        Id = item.ProductId,
                        Avalible = item.Qauntity > 0 && item.Qauntity > requestedItem.Qauntity,
                        Reason=""
                    });
                }
                else
                    list.Add(new ProductAvaliblity
                    {
                        Id = item.ProductId,
                        Avalible = false,
                        Reason="wrong product Id"
                    });
            }
            return list;
        }
    }
}
