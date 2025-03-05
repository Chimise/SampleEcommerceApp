using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    internal class StubCommandService<TCommand>: ICommandHandler<TCommand> where TCommand : class
    {
        public Task ExecuteAsync(TCommand command)
        {
            return Task.CompletedTask;
        }
    }
}
