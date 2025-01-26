using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.AuditEntries
{
    public class AuditEntry
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime TimeOfExecution { get; set; }
        public required string Operation { get; set; }
        public required string Data { get; set; }
    }
}
