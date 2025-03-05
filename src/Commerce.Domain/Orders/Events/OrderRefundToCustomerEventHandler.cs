using Commerce.Domain.Common;
using Commerce.Domain.ProductInventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.Orders.Repositories;

namespace Commerce.Domain.Orders.Events
{
    internal class OrderRefundToCustomerEventHandler: IEventHandler<OrderCancelled>, IEventHandler<OrderPayed> 
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBillingService _billingService;

        public OrderRefundToCustomerEventHandler(IOrderRepository orderRepository, IBillingService billingService)
        {
            ArgumentNullException.ThrowIfNull(orderRepository, nameof(orderRepository));
            ArgumentNullException.ThrowIfNull(billingService, nameof(billingService));
            _orderRepository = orderRepository;
            _billingService = billingService;
        }

        public void Handle(OrderCancelled e)
        {
            Order order = _orderRepository.GetById(e.OrderId).GetAwaiter().GetResult();
            if(order.Payed)
            {
                _billingService.NotifyAccounting(e.OrderId, "RequestRefund");
            }
        }

        public void Handle(OrderPayed e)
        {
            Order order = _orderRepository.GetById(e.OrderId).GetAwaiter().GetResult();
            if(order.Cancelled)
            {
                _billingService.NotifyAccounting(e.OrderId, "RequestRefund");
            }
        }
    }
}
