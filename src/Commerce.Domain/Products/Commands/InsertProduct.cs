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
    public class InsertProduct
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        [Required, StringLength(50)]
        public required string Name { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
