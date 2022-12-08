using MediatR;
using Microsoft.AspNetCore.Mvc;
using InventoryApplication.Queries;
using InventoryAppliction;
using InventoryAppliction.Commands;
using InventoryAppliction.Models;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<InventoryController> _logger;
        private readonly IMediator _mediator;

        public InventoryController(ILogger<InventoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IEnumerable<InventoryModel>> GetAsync()
        {
            return await _mediator.Send(new GetAllInventorysQuery());

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<InventoryModel> GetByIdAsync(int id)
        {
            return await _mediator.Send(new GetInventoryByIdQuery(id));

        }
        [HttpPost]
        public async Task<bool> PostAsync(InventoryModel Inventory)
        {
            try
            {
                await _mediator.Send(new AddInventoryCommand(Inventory));

                _logger.LogInformation("save Inventory:" + Inventory.Qauntity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpPost]
        [Route("CehckAvalibleProductQuntity")]

        public async Task<List<ProductAvaliblity>> CehckAvalibleProductQuntity([FromBody] List<ProductModel> Products)
        {
               return await _mediator.Send(new CheckAvalibleProductQuntityQuery(Products));
        }
    }
}