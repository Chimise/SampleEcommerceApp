using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.Products;
using Commerce.Domain.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Commerce.SqlDataAccess
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly CommerceContext _dbContext;
        public SqlProductRepository(CommerceContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            _dbContext = dbContext;
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if(product == null) throw new KeyNotFoundException($"No product with {id} was found");

            return product;
        }

        public void Save(Product product)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            if(_dbContext.IsNew(product))
            {
                _dbContext.Products.Add(product);
            }
        }
        public async Task Delete(Guid id)
        {
            var product = await this.GetById(id);
            _dbContext.Remove(product);
        }
        public async Task<Product[]> GetAll()
        {
            return await _dbContext.Products.ToArrayAsync();
        }
    }
}
