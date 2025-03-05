using Commerce.Domain.TermsAndCondition.Repositories;
using Commerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess
{
    public class SqlTermsRepository : ITermsRepository
    {
        private readonly CommerceContext context;
        private readonly ITimeProvider timeProvider;

        public SqlTermsRepository(CommerceContext context, ITimeProvider timeProvider)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (timeProvider == null) throw new ArgumentNullException(nameof(timeProvider));

            this.context = context;
            this.timeProvider = timeProvider;
        }

        public string GetActiveTerms()
        {
            return "Hello, our terms and conditions are simple: Don't be evil";
        }
    }
}
