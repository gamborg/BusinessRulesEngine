using BusinessRulesEngine.Handlers.BusinessRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Models
{
    public class Payment
    {
        public Guid OrderId { get; set; } 
        public List<Product> Products { get; set; }
        public Guid Agent { get; set; }
        public Customer Customer { get; set; }
    }
}
