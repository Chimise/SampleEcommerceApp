using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.ProductInventories
{
    public interface ILocationService
    {
        Warehouse[] FindWarehouses();
    }

    public class Warehouse { }
}
