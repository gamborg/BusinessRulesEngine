using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.MediatR.Notifications.Membership;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Handlers.BusinessRules
{
    public class MembershipEmailOwnerUpgraded : IRule
    {
        private readonly IMediator _mediator;
        public MembershipEmailOwnerUpgraded(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Apply(Payment payment)
        {
            _mediator.Publish(new SendUpgradeEmailNotification
            {
                RecipientEmail = payment.Customer.Email,
                RecipientName = payment.Customer.Name
            });
            return Task.CompletedTask;
        }
    }
}
