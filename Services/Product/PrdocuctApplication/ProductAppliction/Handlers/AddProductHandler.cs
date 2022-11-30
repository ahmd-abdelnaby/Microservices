using MediatR;
using ProductAppliction.Commands;
using ProductAppliction.Events;
using Serilog;

namespace ProductAppliction.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, ProductModel>
    {
        private readonly ILogger _logger;
        private readonly IPublisher _publisher;

        public AddProductHandler(ILogger logger, IPublisher publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        public async Task<ProductModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            _logger.Information("insert Product");

            await _publisher.Publish(new ProductAddedEvent { Id = request.productModel.id, Name = request.productModel.Name }, cancellationToken) ;

            return request.productModel;
        }
    }
}