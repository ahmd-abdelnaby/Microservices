using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApplication;
using OrderApplication.Commands;
using OrderApplication.Queries;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [Authorize(Policy = "Consumer")]
        [Authorize(Roles = "consumer")]
        [HttpGet]
        public async Task<IEnumerable<OrderModel>> GetAsync()
        {
            return await _mediator.Send(new GetAllOrdersQuery());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<OrderModel> GetById(int id)
        {
            return await _mediator.Send(new GetOrderByIdQuery(id));

        }
        [HttpPost]
        public async Task<bool> PostAsync(OrderModel order)
        {
            try
            {
                await _mediator.Send(new AddOrderCommand(order));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}