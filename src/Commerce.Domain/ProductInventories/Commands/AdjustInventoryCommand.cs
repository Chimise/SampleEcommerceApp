using Commerce.Domain.Common;
using Commerce.Domain.Common.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.ProductInventories.Commands
{
    [PermittedRole(Role.InventoryManager)]
    public class AdjustInventoryCommand
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        public bool Decrease { get; set; }

        [Range(minimum: 1, maximum: 10000)]
        public int Quantity { get; set; }
    }
}
