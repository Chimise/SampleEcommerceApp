using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Common
{
    public class CompositeEventHandler<TEvent>: IEventHandler<TEvent> where TEvent : class
    {
        private readonly IEnumerable<IEventHandler<TEvent>> _handlers;
        public CompositeEventHandler(IEnumerable<IEventHandler<TEvent>> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(TEvent e) {
            foreach(var handler in _handlers)
            {
                handler.Handle(e);
            }
        }
    }
}
