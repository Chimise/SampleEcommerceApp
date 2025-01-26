using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.ProductInventories;

namespace Commerce.Domain.ProductInventories.Repositories
{
    public interface IInventoryRepository
    {
        Task<ProductInventory> GetByIdOrNull(Guid id);
        Task Save(ProductInventory productInventory);
    }
}
