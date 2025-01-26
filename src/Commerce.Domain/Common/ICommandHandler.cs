using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Common
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task ExecuteAsync(TCommand command);
    }
}
