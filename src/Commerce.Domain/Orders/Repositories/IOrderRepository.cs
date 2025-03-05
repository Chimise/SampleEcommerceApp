using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetById(Guid id);
        void Save(Order order);
    }
}
