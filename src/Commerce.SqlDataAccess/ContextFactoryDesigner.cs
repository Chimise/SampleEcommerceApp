using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess
{
    internal class ContextFactoryDesigner : IDesignTimeDbContextFactory<CommerceContext>
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";

        public CommerceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommerceContext>();
            optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("Commerce.SqlDataAccess"));

            return new CommerceContext(optionsBuilder.Options);
        }
    }
}
