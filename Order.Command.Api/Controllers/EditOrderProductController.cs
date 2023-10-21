using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Command.Api.Commands;
using Order.Common.DTOs;

namespace Order.Command.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EditOrderProductController : ControllerBase
    {
        private readonly ILogger<EditOrderProductController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public EditOrderProductController(ILogger<EditOrderProductController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{id}/{productId}/{count}/{currentPrice}/{currentDiscount}/{inventoryCount}")]
        public async Task<ActionResult> EditProductAsync(Guid id, Guid productId, uint count, uint currentPrice, uint currentDiscount, uint inventoryCount, ChangeOrderProductCountCommand command)
        {
            try
            {
                command.Id = id;
                command.ProductId = productId;
                command.CurrentPrice = currentPrice;
                command.CurrentDiscount = currentDiscount;
                command.CurrentPrice = currentPrice;
                command.InventoryCount = inventoryCount;
                command.Count = count;
                await _commandDispatcher.SendAsync(command);

                return Ok(new BaseResponse
                {
                    Message = "Changing product count request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (AggregateNotFoundException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect order ID targetting the aggregate!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request changing the count of a product";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
