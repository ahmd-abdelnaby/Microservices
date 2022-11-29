using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderApplication.Queries;
using ProductApplication.Queries;
using ProductAppliction;
using ProductAppliction.Commands;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetAsync()
        {
            throw new Exception("some error throw");
            return await _mediator.Send(new GetAllProductsQuery());

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ProductModel> GetByIdAsync(int id)
        {
            return await _mediator.Send(new GetProductByIdQuery(id));

        }
        [HttpPost]
        public async Task<bool> PostAsync(ProductModel product)
        {
            try
            {
                await _mediator.Send(new AddProductCommand(product));

                _logger.LogInformation("save product:" + product.Name);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}