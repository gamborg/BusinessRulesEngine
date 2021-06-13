using BusinessRulesEngine.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.MediatR.Notifications.PhysicalProduct
{
    public class GenerateCommissionToAgentNotification : INotification
    {
        public Payment Payment { get; set; }
        public Guid AgentId { get; set; }
    }
}
