using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        void Save(Product product);
        void Delete(Guid id);
        Product[] GetAll();
    }
}
