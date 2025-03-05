using Commerce.Domain.ProductInventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.ExternalConnections
{
    public class WCFLocationService: ILocationService
    {
        public Warehouse[] FindWarehouses() => new[] { new Warehouse() }; 
    }
}
