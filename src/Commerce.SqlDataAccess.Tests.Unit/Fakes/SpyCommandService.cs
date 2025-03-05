using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.Common;
using Xunit;

namespace Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    internal class SpyCommandService<TCommand> : ICommandHandler<TCommand> where TCommand : class
    {
        public List<TCommand> ExecutedCommands { get; } = new List<TCommand>();

        public bool ExecutedOnce => ExecutedCommands.Count == 1;

        public event Action Executed = () => { };

        public Task ExecuteAsync(TCommand command)
        {
            Assert.NotNull(command);

            this.ExecutedCommands.Add(command);

            this.Executed();

            return Task.CompletedTask;
        }
    }
}
