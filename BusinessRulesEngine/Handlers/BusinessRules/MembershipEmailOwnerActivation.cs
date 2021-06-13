using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.MediatR.Notifications.Membership;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Handlers.BusinessRules
{
    public class MembershipEmailOwnerActivation : IRule
    {
        private readonly IMediator _mediator;
        public MembershipEmailOwnerActivation(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Apply(Payment payment)
        {
            _mediator.Publish(new SendActivationEmailNotification
            {
                RecipientEmail = payment.Customer.Email,
                RecipientName = payment.Customer.Name
            });
            return Task.CompletedTask;
        }
    }
}
