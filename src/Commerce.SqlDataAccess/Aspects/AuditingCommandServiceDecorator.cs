using Commerce.Domain.Common;
using Commerce.Domain.Users;
using Commerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Commerce.Domain.AuditEntries;


namespace Commerce.SqlDataAccess.Aspects
{
    public class AuditingCommandServiceDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
        private readonly IUserContext _userContext;
        private readonly CommerceContext _commerceContext;
        private readonly ITimeProvider _timeProvider;
        private readonly ICommandHandler<TCommand> _decoratee;

        public AuditingCommandServiceDecorator(IUserContext userContext, CommerceContext commerceContext, ITimeProvider timeProvider, ICommandHandler<TCommand> decoratee)
        {
            ArgumentNullException.ThrowIfNull(userContext, nameof(userContext));
            ArgumentNullException.ThrowIfNull(commerceContext, nameof(commerceContext));
            ArgumentNullException.ThrowIfNull(timeProvider, nameof(timeProvider));
            ArgumentNullException.ThrowIfNull(decoratee, nameof(decoratee));

            _userContext = userContext;
            _commerceContext = commerceContext;
            _timeProvider = timeProvider;
            _decoratee = decoratee;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            await _decoratee.ExecuteAsync(command);
            this.AppendToAuditTrail(command);

        }

        private void AppendToAuditTrail(TCommand command)
        {

            var entry = new AuditEntry
            {
                UserId = _userContext.CurrentUserId,
                TimeOfExecution = _timeProvider.Now,
                Operation = command.GetType().Name,
                Data = JsonSerializer.Serialize(command)
            };

            _commerceContext.AuditEntries.Add(entry);
        }
    }

}

