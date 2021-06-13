using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRulesEngine.Handlers.PaymentTypes
{
    public class Video : PhysicalProduct
    {
        public Video(IMediator mediator, IDbContext dbContext) : base(mediator, dbContext)
        {
            HandlerName = "video";

            Rules = Rules.Prepend(new AddFirstAidVideo()).ToList();
        }
    }
}
