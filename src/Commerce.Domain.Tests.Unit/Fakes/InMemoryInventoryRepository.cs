using Commerce.Domain.ProductInventories;
using Commerce.Domain.ProductInventories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Tests.Unit.Fakes
{
    internal class InMemoryInventoryRepository: IInventoryRepository
    {
        private readonly Dictionary<Guid, ProductInventory> _inventories = new Dictionary<Guid, ProductInventory>();

        public Task<ProductInventory?> GetByIdOrNull(Guid id)
        {
            var productInventory = _inventories.TryGetValue(id, out var value) ? value : null;
            return Task.FromResult(productInventory);
        }

        public void Save(ProductInventory inventory)
        {
            _inventories[inventory.Id] = inventory;
        }
    }
}
