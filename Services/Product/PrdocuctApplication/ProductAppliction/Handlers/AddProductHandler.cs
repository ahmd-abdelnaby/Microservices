using MediatR;
using ProductAppliction.Commands;
using Serilog;

namespace ProductAppliction.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, ProductModel>
    {
        private readonly ILogger _logger;

        public AddProductHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<ProductModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("insert Product");
            return request.productModel;
        }
    }
}