using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplication.Queries;
using PaymentAppliction;
using PaymentAppliction.Commands;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {

        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;

        public PaymentController(ILogger<PaymentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IEnumerable<PaymentModel>> GetAsync()
        {
            return await _mediator.Send(new GetAllPaymentsQuery());

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<PaymentModel> GetByIdAsync(int id)
        {
            return await _mediator.Send(new GetPaymentByIdQuery(id));

        }
        [HttpPost]
        public async Task<bool> PostAsync(PaymentModel Payment)
        {
            try
            {
                await _mediator.Send(new AddPaymentCommand(Payment));

                _logger.LogInformation("save Payment:" + Payment.Amount);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}