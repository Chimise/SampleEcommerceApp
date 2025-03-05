using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.TermsAndCondition
{
    public interface IMessageService
    {
        void SendTermsAndConditions(string mailAddress, string text);
    }
}
