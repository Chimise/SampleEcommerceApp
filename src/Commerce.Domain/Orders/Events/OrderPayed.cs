﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Events
{
    public class OrderPayed
    {
        public Guid OrderId;

        public OrderPayed(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}
