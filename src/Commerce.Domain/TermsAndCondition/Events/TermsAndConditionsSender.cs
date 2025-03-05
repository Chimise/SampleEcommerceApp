using Commerce.Domain.Common;
using Commerce.Domain.Crm.Events;
using Commerce.Domain.TermsAndCondition.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.TermsAndCondition.Events
{
    public class TermsAndConditionsSender: IEventHandler<CustomerCreated>
    {
        private readonly IMessageService messageService;
        private readonly ITermsRepository repository;
        public TermsAndConditionsSender(
            IMessageService messageService, ITermsRepository repository)
        {
            if (messageService == null) throw new ArgumentNullException(nameof(messageService));
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.messageService = messageService;
            this.repository = repository;
        }

        public void Handle(CustomerCreated e)
        {
            string text = this.repository.GetActiveTerms();

            this.messageService.SendTermsAndConditions(e.MailAddress, text);
        }
    }
}
