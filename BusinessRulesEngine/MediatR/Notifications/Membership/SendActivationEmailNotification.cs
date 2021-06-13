using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.MediatR.Notifications.Membership
{
    public class SendActivationEmailNotification : INotification
    {
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
    }
}
