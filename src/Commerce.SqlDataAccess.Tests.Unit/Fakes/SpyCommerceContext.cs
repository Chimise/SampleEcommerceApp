using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    internal class SpyCommerceContext: CommerceContext
    {
        private static DbContextOptions<CommerceContext> _options = 
            new DbContextOptionsBuilder<CommerceContext>().UseSqlServer("dummy value").Options;
        public SpyCommerceContext(): base(_options)
        {

        }

        public bool HasChanged { get; private set; }

        public override int SaveChanges()
        {
            this.HasChanged = true;
            return 0;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.HasChanged = true;
            return Task.FromResult(0);
        }
    }
}
