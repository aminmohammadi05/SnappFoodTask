using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Order.Command.Api.Commands;
using Order.Command.Api.DTOs;
using Order.Common.DTOs;

namespace Order.Command.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewOrderController : ControllerBase
    {
        private readonly ILogger<NewOrderController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewOrderController(ILogger<NewOrderController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> NewOrderAsync(NewOrderCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewOrderResponse
                {
                    Message = "New order creation request completed successfully!"
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
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new order!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new NewOrderResponse
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
