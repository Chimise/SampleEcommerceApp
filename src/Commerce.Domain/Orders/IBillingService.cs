using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders
{
    public interface IBillingService
    {
        void NotifyAccounting(Guid orderId, string notification);
    }
}
