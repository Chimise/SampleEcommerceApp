using Commerce.Domain.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.ExternalConnections
{
    public class WCFCrmSystem : ICrmSystem
    {
        // Calls the web service of the CRM service.
        public void CustomerCreated(Guid customerId)
        {
            // Calls the web service of the external CRM system.
        }
    }
}
