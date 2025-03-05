using Commerce.Domain.Orders;
using System.Diagnostics;


namespace Commerce.ExternalConnections
{
    public class WCFBillingSystem: IBillingService
    {
        public void NotifyAccounting(Guid orderId, string notification)
        {
            Debug.Write($"Accounting notified for order id {orderId} and notification '{notification}'");
        }
    }
}
