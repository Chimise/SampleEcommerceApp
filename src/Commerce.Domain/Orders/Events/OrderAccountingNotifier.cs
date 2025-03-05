using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Events
{
    internal class OrderAccountingNotifier: IEventHandler<OrderApproved>, IEventHandler<OrderCancelled>
    {
        private readonly IBillingService _billingService;

        public OrderAccountingNotifier(IBillingService billingService)
        {
            ArgumentNullException.ThrowIfNull(billingService);
            _billingService = billingService;
        }

        public void Handle(OrderApproved e)
        {
            _billingService.NotifyAccounting(e.OrderId, "Approved");
        }

        public void Handle(OrderCancelled e)
        {
            _billingService.NotifyAccounting(e.OrderId, "Cancelled");
        }
    }
}
