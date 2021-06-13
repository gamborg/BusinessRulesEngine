using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Interfaces
{
    public interface IBusinessRuleHandler
    {
        public string HandlerName { get; }
        public void Execute(Payment payment);
    }
}
