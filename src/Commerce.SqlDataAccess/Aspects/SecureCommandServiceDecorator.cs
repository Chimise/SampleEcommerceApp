using Commerce.Domain.Common;
using Commerce.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess.Aspects
{
    public class SecureCommandServiceDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
        private static readonly Role PermittedRole = GetPermittedRole();
        private readonly IUserContext _userContext;
        private readonly ICommandHandler<TCommand> _decoratee;

        public SecureCommandServiceDecorator(IUserContext userContext, ICommandHandler<TCommand> decoratee)
        {
            ArgumentNullException.ThrowIfNull(userContext, nameof(userContext));
            ArgumentNullException.ThrowIfNull(decoratee, nameof(decoratee));

            _userContext = userContext;
            _decoratee = decoratee;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            this.CheckAuthorization();

            await _decoratee.ExecuteAsync(command);
        }

        private void CheckAuthorization()
        {
            if (!_userContext.IsInRole(PermittedRole))
            {
                throw new SecurityException();
            }
        }

        private static Role GetPermittedRole()
        {
            var attribute = typeof(TCommand).GetCustomAttribute<PermittedRoleAttribute>();
            if (attribute == null)
            {
                throw new InvalidOperationException($"[PermittedRole] missing from {typeof(TCommand).Name}.");
            }

            return attribute.Role;

        }
    }
}
