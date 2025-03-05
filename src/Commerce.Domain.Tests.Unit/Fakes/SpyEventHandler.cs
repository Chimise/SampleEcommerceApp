using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Commerce.Domain.Tests.Unit.Fakes
{
    internal class SpyEventHandler<TEvent>: IEventHandler<TEvent> where TEvent : class
    {
        public List<TEvent> HandledEvents { get; private set; } = new List<TEvent>();

        public TEvent HandledEvent 
        { 
            get
            {
                Assert.Single(HandledEvents);
                return this.HandledEvents[0];
            } 
        }

        public void Handle(TEvent e)
        {
            Assert.NotNull(e);
            this.HandledEvents.Add(e);
        }
    }
}
