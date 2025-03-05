using Commerce.Domain.Common;
using Commerce.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    internal class StubUserContext : IUserContext
    {
        public Guid CurrentUserId { get; set; }

        public bool IsInRole(Role permittedRole) => true;
    }
}
