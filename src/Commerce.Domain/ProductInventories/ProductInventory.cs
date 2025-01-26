using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.ProductInventories
{
    public class ProductInventory
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
