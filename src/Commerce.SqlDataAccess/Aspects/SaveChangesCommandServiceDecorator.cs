using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess.Aspects
{
    public class SaveChangesCommandServiceDecorator<TCommand>: ICommandHandler<TCommand> where TCommand: class
    {
        private readonly CommerceContext _context;
        private readonly ICommandHandler<TCommand> _decoratee;

        public SaveChangesCommandServiceDecorator(CommerceContext context, ICommandHandler<TCommand> decoratee)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(decoratee, nameof(decoratee));

            _context = context;
            _decoratee = decoratee;
        }

        public async Task ExecuteAsync(TCommand command)
        {
            await _decoratee.ExecuteAsync(command);
            await _context.SaveChangesAsync();
        }
    }
}
