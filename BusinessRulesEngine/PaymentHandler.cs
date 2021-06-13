using BusinessRulesEngine.Handlers;
using BusinessRulesEngine.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine
{
    public class PaymentHandler
    {
        private readonly BusinessRulesFactory businessRulesFactory;

        public PaymentHandler(IServiceProvider serviceProvider)
        {
            businessRulesFactory = new BusinessRulesFactory(serviceProvider);
        }

        public void Handle(string paymentType, Payment payment)
        {
            var handler = businessRulesFactory.GetProviderByName(paymentType);
            if (handler != null)
            {
                handler.Execute(payment);
            }
        }
    }
}
