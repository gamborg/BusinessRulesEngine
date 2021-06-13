using MediatR;

namespace BusinessRulesEngine.MediatR.Notifications.Membership
{
    public class SendUpgradeEmailNotification : INotification
    {
        public string RecipientName { get; set; }
        public string RecipientEmail { get; set; }
    }
}
