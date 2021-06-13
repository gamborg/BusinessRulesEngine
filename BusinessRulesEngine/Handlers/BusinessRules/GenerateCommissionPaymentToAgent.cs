using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.MediatR.Notifications.PhysicalProduct;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Handlers.BusinessRules
{
    public class GenerateCommissionPaymentToAgent : IRule
    {
        public GenerateCommissionPaymentToAgent(IMediator mediator)
        {
            Mediator = mediator;
        }

        private IMediator Mediator { get; }

        public Task Apply(Payment payment)
        {
            Mediator.Publish(new GenerateCommissionToAgentNotification
            {
                AgentId = payment.Agent,
                Payment = payment
            });

            return Task.CompletedTask;
        }

    }
}
