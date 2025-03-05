using Commerce.Api.Common;
using Commerce.Domain.Common;
using Commerce.Domain.Orders.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Api.Controllers
{
    
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandHandler<ApproveOrder> _orderApprover;
        private readonly ICommandHandler<CancelOrder> _orderCancellor;
        public OrderController(ICommandHandler<CancelOrder> orderCancellor, ICommandHandler<ApproveOrder> orderApprover)
        {
            ArgumentNullException.ThrowIfNull(orderCancellor, nameof(orderCancellor));
            ArgumentNullException.ThrowIfNull(orderApprover, nameof(orderApprover));

            _orderCancellor = orderCancellor;
            _orderApprover = orderApprover;
        }

        [HttpPost("api/orders/approve")]
        public async Task<ActionResult<GenericResponse>> Approve(ApproveOrder command)
        {
            await _orderApprover.ExecuteAsync(command);
            return Ok(GenericResponse.Ok(successMessage: "Order approved successfully"));
        }

        [HttpPost("api/orders/cancel")]
        public async Task<ActionResult<GenericResponse>> Cancel(CancelOrder command)
        {
            await _orderCancellor.ExecuteAsync(command);
            return Ok(GenericResponse.Ok(successMessage: "Order cancelled successfully"));
        }
    }
}
