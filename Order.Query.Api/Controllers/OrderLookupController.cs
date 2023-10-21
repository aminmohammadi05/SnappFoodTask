using CQRS.Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Common.DTOs;
using Order.Query.Api.DTOs;
using Order.Query.Domain.Entities;
using Order.Query.Infrastructure.Features.Orders.Queries.GetOrdersList;

namespace Order.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderLookupController : ControllerBase
    {
        private readonly ILogger<OrderLookupController> _logger;
        private readonly IMediator _mediator;
        private readonly IQueryDispatcher<OrderEntity> _queryDispatcher;

        public OrderLookupController(ILogger<OrderLookupController> logger, IMediator mediator, IQueryDispatcher<OrderEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _mediator.Send(new GetOrdersListQuery());
                return NormalResponse(orders);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all orders!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("byId/{orderId}")]
        public async Task<ActionResult> GetByOrderIdAsync(Guid orderId)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery { Id = orderId });

                if (order == null )
                    return NoContent();

                return Ok(new OrderLookupResponse
                {
                    Orders = new List<OrderEntity> { order },
                    Message = "Successfully returned order!"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find order by ID!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("byBuyer/{buyer}")]
        public async Task<ActionResult> GetOrdersByAuthorAsync(string buyer)
        {
            try
            {
                var orders = await _mediator.Send(new GetOrderByBuyerQuery { user = buyer });
                return NormalResponse(orders);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to find orders by criteria!";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        

       

        private ActionResult NormalResponse(List<OrderEntity> orders)
        {
            if (orders == null || !orders.Any())
                return NoContent();

            var count = orders.Count;
            return Ok(new OrderLookupResponse
            {
                Orders = orders,
                Message = $"Successfully returned {count} order{(count > 1 ? "s" : string.Empty)}!"
            });
        }

        private ActionResult ErrorResponse(Exception ex, string safeErrorMessage)
        {
            _logger.LogError(ex, safeErrorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = safeErrorMessage
            });
        }
    }
}
