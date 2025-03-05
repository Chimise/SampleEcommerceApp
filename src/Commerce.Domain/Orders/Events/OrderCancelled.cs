using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Events
{
    public class OrderCancelled
    {
        public Guid OrderId;

        public OrderCancelled(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
