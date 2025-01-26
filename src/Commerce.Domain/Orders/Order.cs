using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Orders
{
    public class Order
    {
        public Guid Id { get; set; }
        public bool Approved { get; private set; }
        public bool Cancelled { get; private set; }
        public bool Payed { get; private set; }
        public void Approve()
        {
            Approved = true;
        }

        internal void Cancel()
        {
            Cancelled = true;
        }
    }
}
