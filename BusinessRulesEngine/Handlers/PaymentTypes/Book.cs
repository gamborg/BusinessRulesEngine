using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Handlers.PaymentTypes
{
    public class Book : PhysicalProduct
    {
        public Book(IMediator mediator, IDbContext dbContext) : base(mediator, dbContext)
        {
            HandlerName = "book";
            base.Rules.Add(new GeneratePackingSlipForShipping("royalty department", dbContext));
        }
    }
}
