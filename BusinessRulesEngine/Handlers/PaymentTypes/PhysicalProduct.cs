using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace BusinessRulesEngine.Handlers.PaymentTypes
{
    public class PhysicalProduct : BaseProduct
    {
        public PhysicalProduct(IMediator mediator, IDbContext dbContext)
        {
            HandlerName = "physical";
            Rules.Add(new GeneratePackingSlipForShipping("shipping department", dbContext));
            Rules.Add(new GenerateCommissionPaymentToAgent(mediator));
        }
    }
}
