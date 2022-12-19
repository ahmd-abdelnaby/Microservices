using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrderApplication;
using OrderApplication.Commands;
using OrderApplication.DTO;
using OrderApplication.Queries;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [DisableCors]
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
        [Route("AddOrder")]
        public async Task<bool> PostAsync(OrderDto order)
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