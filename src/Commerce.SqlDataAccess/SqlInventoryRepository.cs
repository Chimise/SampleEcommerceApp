using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.ProductInventories;
using Commerce.Domain.ProductInventories.Repositories;

namespace Commerce.SqlDataAccess
{
    public class SqlInventoryRepository: IInventoryRepository
    {
        private readonly CommerceContext _context;
        public SqlInventoryRepository(CommerceContext context)
        {
            _context = context;
        }

        public void Save(ProductInventory inventory)
        {
            if(_context.IsNew(inventory))
            {
                _context.ProductInventories.Add(inventory);
            }
        }

        public async Task<ProductInventory?> GetByIdOrNull(Guid id)
        {
            var productInventory = await _context.ProductInventories.FindAsync(id);
            return productInventory;
        }
    }
}
