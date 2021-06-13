using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Handlers.PaymentTypes
{
    public abstract class BaseProduct : IBusinessRuleHandler
    {
        public string HandlerName { get; set; }
        public List<IRule> Rules { get; set; } = new List<IRule>();

        public virtual void Execute(Payment payment)
        {
            foreach (var rule in Rules)
            {
                rule.Apply(payment);
            }
        }

    }
}
