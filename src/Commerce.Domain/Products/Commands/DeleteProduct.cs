using Commerce.Domain.Common.ValidationAttributes;
using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products.Commands
{
    [PermittedRole(Role.Administrator)]
    public class DeleteProduct
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
    }
}
