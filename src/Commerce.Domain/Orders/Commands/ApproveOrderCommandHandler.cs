using Commerce.Domain.Common;
using Commerce.Domain.Orders.Events;
using Commerce.Domain.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Commands
{
    public class ApproveOrderCommandHandler: ICommandHandler<ApproveOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventHandler<OrderApproved> _handler;

        public ApproveOrderCommandHandler(IOrderRepository orderRepository, IEventHandler<OrderApproved> handler)
        {
            ArgumentNullException.ThrowIfNull(orderRepository, nameof(orderRepository));
            ArgumentNullException.ThrowIfNull(handler, nameof(handler));

            _orderRepository = orderRepository;
            _handler = handler;
        }

        public Task ExecuteAsync(ApproveOrder command)
        {
            var order = _orderRepository.GetById(command.OrderId);
            order.Approve();
            _orderRepository.Save(order);

            _handler.Handle(new OrderApproved(command.OrderId));

            return Task.CompletedTask;
            
        }
    }
}
