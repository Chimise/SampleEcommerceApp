using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.Products;
using Commerce.Domain.Products.Repositories;

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

        public Product GetById(Guid id)
        {
            return _dbContext.Products.Find(id) ?? throw new KeyNotFoundException($"No product with {id} was found");
        }

        public void Save(Product product)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            if(!_dbContext.IsNew(product))
            {
                _dbContext.Products.Add(product);
            }
        }
        public void Delete(Guid id)
        {
            var product = this.GetById(id);
            _dbContext.Remove(product);
        }
        public Product[] GetAll()
        {
            return _dbContext.Products.ToArray();
        }
    }
}
