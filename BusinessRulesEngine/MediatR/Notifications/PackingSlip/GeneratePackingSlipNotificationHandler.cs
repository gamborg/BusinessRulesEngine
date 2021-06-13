using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessRulesEngine.MediatR.Notifications.PackingSlip
{
    public class GeneratePackingSlipNotificationHandler : INotificationHandler<GeneratePackingSlipNotification>
    {
        public async Task Handle(GeneratePackingSlipNotification notification, CancellationToken cancellationToken)
        {
            // Send packing info to external service for actual printing
        }
    }
}
