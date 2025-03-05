using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Events
{
    public class OrderApproved
    {
        public readonly Guid OrderId;

        public OrderApproved(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
