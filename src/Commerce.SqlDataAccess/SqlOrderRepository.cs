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
        public async Task<Order> GetById(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if(order == null) throw new KeyNotFoundException($"No Order with Id {id} was found");
            return order;
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
