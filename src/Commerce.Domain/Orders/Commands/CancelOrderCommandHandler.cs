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
    public class CancelOrderCommandHandler: ICommandHandler<CancelOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventHandler<OrderCancelled> _handler;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IEventHandler<OrderCancelled> handler)
        {
            ArgumentNullException.ThrowIfNull(orderRepository, nameof(orderRepository));
            ArgumentNullException.ThrowIfNull(handler, nameof(handler));

            _orderRepository = orderRepository;
            _handler = handler;
        }
        public async Task ExecuteAsync(CancelOrder command)
        {
            var order = await _orderRepository.GetById(command.OrderId);
            order.Cancel();
            _orderRepository.Save(order);
            _handler.Handle(new OrderCancelled(command.OrderId));
        }
    }
}
