using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.ProductInventories.Events
{
    public class InventoryAdjusted
    {
        public readonly Guid ProductId;
        public readonly decimal QuantityAdjustment;

        public InventoryAdjusted(Guid productId, decimal quantityAdjustment)
        {
            ProductId = productId;
            QuantityAdjustment = quantityAdjustment;
        }
    }
}
