using Commerce.Domain.Common;
using Commerce.Domain.Common.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Commands
{
    [PermittedRole(Role.OrderManager)]
    public class CancelOrder
    {
        [RequiredGuid]
        public Guid OrderId { get; set; }
    }
}
