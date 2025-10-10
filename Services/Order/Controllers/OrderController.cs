using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Commands;
using Ordering.DTOs;
using Ordering.Mappers;
using Ordering.Queries;

namespace Ordering.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{userName}", Name = "GetOrdersByUserName")]
        public async Task<ActionResult<OrderDTO>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrderListQuery(userName);
            var orders = await _mediator.Send(query);
            _logger.LogInformation($"Orders fetched for user :{userName}");

            return Ok(orders);



        }

        [HttpPost(Name = "CheckOutOrder")]
        public async Task<ActionResult<Guid>> CheckOutOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var cmd = createOrderDTO.ToCommand();
            var result = await _mediator.Send(cmd);

            _logger.LogInformation($"Order created with Id {result}");

            return Ok(result);


        }
        [HttpPut(Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDTO updateOrderDTO)
        {
            var cmd = updateOrderDTO.ToCommand();
            var result = await _mediator.Send(cmd);
            _logger.LogInformation($"Successfully Updated {updateOrderDTO.Id}");

            return NoContent();


        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var cmd = new DeleteOrderCommand { Id = id };
            await _mediator.Send(cmd);
            _logger.LogInformation($"Order deleted with Id={id}");
            return NoContent();


        }
    }
}
