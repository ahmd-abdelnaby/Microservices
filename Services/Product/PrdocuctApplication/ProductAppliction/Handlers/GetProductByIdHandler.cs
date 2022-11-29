using MediatR;
using ProductApplication.Queries;
using Serilog;

namespace ProductAppliction.Handlers
{
    internal class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly ILogger _logger;
        public GetProductByIdHandler(ILogger logger)
        {
            _logger = logger;

        }
        public async Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.Information("get product {id}", request.Id);
            return new ProductModel() { Name = "product", id = request.Id, Date = DateTime.Now };
        }
    }
}
