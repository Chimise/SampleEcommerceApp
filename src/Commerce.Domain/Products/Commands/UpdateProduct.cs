using Commerce.Domain.Common.ValidationAttributes;
using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products.Commands
{
    [PermittedRole(Role.Administrator)]
    public class UpdateProduct
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }

        [MinLength(1), StringLength(50)]
        public string? Name { get; set; }
        public decimal? UnitPrice { get; set; }
        [MinLength(1), StringLength(50)]
        public string? Description { get; set; }
    }
}
