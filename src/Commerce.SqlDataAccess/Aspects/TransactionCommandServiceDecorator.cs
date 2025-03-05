using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Commerce.SqlDataAccess.Aspects
{
    public class TransactionCommandServiceDecorator<TCommand>: ICommandHandler<TCommand> where TCommand : class
    {
        private readonly CommerceContext _context;
        private readonly ICommandHandler<TCommand> _decoratee;

        public TransactionCommandServiceDecorator(CommerceContext context, ICommandHandler<TCommand> decoratee)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(decoratee, nameof(decoratee));

            _context = context;
            _decoratee = decoratee;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            ArgumentNullException.ThrowIfNull(_context, nameof(_context));

            using(var scope = new TransactionScope())
            {
                await _decoratee.ExecuteAsync(command);
                scope.Complete();
            }
        }
    }
}
