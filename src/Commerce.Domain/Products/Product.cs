using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsFeatured { get; set; }
        public bool HasTierPrices { get; set; }
    }
}
