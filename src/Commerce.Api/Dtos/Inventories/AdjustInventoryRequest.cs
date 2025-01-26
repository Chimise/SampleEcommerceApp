using System.ComponentModel.DataAnnotations;
using System;
using Commerce.Domain.Common.ValidationAttributes;

namespace Commerce.Api.Dtos.Inventories
{
    public class AdjustInventoryRequest
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        public bool Decrease { get; set; }

        [Range(minimum: 1, maximum: 10000)]
        public int Quantity { get; set; }
    }
}
