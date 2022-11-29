using MediatR;
using OrderApplication.Queries;
using Serilog;

namespace ProductAppliction.Handlers
{
    internal class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductModel>>
    {
        private readonly ILogger _logger;
        public GetAllProductsHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<List<ProductModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get all orders");
            return Enumerable.Range(1, 5).Select(index => new ProductModel
            {
                Date = DateTime.Now.AddDays(index),
                Name = "product",
                id = 1

            }).ToList();
        }
    }
}
