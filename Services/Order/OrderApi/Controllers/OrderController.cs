using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApplication;
using OrderApplication.Commands;
using OrderApplication.Queries;
using OrderApplication.ViewModels;

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
        public async Task<IEnumerable<Order>> GetAsync()
        {
            return await _mediator.Send(new GetAllOrdersQuery());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<Order> GetById(int id)
        {
            return await _mediator.Send(new GetOrderByIdQuery(id));

        }
        [HttpPost]
        public async Task<bool> PostAsync(OrderVM order)
        {
            try
            {
                
                return await _mediator.Send(new AddOrderCommand(order));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}