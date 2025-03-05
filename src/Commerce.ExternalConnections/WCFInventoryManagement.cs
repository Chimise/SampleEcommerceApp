using Commerce.Domain.ProductInventories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.ExternalConnections
{
    public class WCFInventoryManagement: IInventoryManagement
    {
        public void NotifyWarehouses(IEnumerable<Warehouse> warehouses)
        {
            Debug.WriteLine("Warehouses notified");
        }
    }
}
