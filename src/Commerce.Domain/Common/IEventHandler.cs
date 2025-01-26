using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Common
{
    public interface IEventHandler<TEvent> where TEvent : class
    {
        void Handle(TEvent e);
    }
}
