using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BusinessRulesEngine.Handlers.PaymentTypes
{
    public class Membership : BaseProduct
    {
        public Membership(IMediator mediator, IDbContext dbContext, IDateTime dateTime)
        {
            HandlerName = "membership";
            Rules.Add(new ActivateMembership(dbContext, dateTime));
            Rules.Add(new MembershipEmailOwnerActivation(mediator));
        }
    }
}
