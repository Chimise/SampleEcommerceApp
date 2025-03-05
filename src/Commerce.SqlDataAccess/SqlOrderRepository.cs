using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.Orders.Repositories;
using Commerce.Domain.Orders;

namespace Commerce.SqlDataAccess
{
    public class SqlOrderRepository: IOrderRepository
    {
        private readonly CommerceContext _context;
        public SqlOrderRepository(CommerceContext context) {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            _context = context;
        }
        public Order GetById(Guid id)
        {
           return _context.Orders.Find(id) ?? throw new KeyNotFoundException($"No Order with Id {id} was found");
        }

        public void Save(Order order) 
        { 
            ArgumentNullException.ThrowIfNull(order, nameof(order));
            
            if(_context.IsNew(order))
            {
                _context.Orders.Add(order);
            }
        }
    }
}
