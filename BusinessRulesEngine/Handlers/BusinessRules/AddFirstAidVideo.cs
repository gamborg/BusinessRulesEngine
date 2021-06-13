using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Handlers.BusinessRules
{
    public class AddFirstAidVideo : IRule
    {
        public Task Apply(Payment payment)
        {
            if (payment.Products.Any(x => x.Name == "Learning to Ski"))
            {
                payment.Products.Add(new Product() { Name = "First Aid", ProductType = "video" });
            }

            return Task.CompletedTask;
        }

    }
}
