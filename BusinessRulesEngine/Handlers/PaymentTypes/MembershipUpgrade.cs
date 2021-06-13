using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Handlers.PaymentTypes
{
    public class MembershipUpgrade : Membership
    {
        public MembershipUpgrade(IMediator mediator, IDbContext dbContext, IDateTime dateTime) : base(mediator, dbContext, dateTime)
        {
            HandlerName = "membershipupgrade";
            base.Rules.Add(new UpgradeMembership(dbContext, dateTime));
            base.Rules.Add(new MembershipEmailOwnerUpgraded(mediator));
        }
    }
}
