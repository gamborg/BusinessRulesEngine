using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.MediatR.Notifications.PackingSlip
{
    public class GeneratePackingSlipNotification : INotification
    {
        public string Department { get; set; }
        public List<Product> Products { get; set; }
    }
}
