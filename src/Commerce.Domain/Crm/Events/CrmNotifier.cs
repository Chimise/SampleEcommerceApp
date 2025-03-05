using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Crm.Events
{
    internal class CrmNotifier
    {
        private readonly ICrmSystem crmSystem;

        public CrmNotifier(ICrmSystem crmSystem)
        {
            if (crmSystem == null) throw new ArgumentNullException(nameof(crmSystem));

            this.crmSystem = crmSystem;
        }

        public void Handle(CustomerCreated e)
        {
            this.crmSystem.CustomerCreated(e.CustomerId);
        }
    }
}
