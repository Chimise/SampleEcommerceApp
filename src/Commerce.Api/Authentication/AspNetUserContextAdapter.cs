using Commerce.Domain.Common;
using Commerce.Domain.Users;

namespace Commerce.Api.Authentication
{
    public class AspNetUserContextAdapter : IUserContext
    {
        private static readonly HttpContextAccessor Accessor = new HttpContextAccessor();

        public Guid CurrentUserId => Guid.NewGuid();

        public bool IsInRole(Role role) => true;
    }
}
