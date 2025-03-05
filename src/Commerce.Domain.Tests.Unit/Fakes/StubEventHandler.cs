using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.Tests.Unit.Fakes
{
    internal class StubEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : class
    {
        public void Handle(TEvent e) { }
    }
}
