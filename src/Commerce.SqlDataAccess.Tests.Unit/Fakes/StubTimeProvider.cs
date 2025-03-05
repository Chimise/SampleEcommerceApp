using Commerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    internal class StubTimeProvider : ITimeProvider
    {
        public DateTime Now { get; set; } = DateTime.Parse("2019-01-01");
    }
}
