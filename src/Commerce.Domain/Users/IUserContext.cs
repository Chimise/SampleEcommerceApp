using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Users
{
    public interface IUserContext
    {
        Guid CurrentUserId { get; }

        bool IsInRole(Role role);
    }
}
