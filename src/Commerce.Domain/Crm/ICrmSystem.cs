using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Crm
{
    public interface ICrmSystem
    {
        void CustomerCreated(Guid customerId);
    }
}
