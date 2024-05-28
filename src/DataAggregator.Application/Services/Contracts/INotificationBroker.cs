using DataAggregator.Domain.Entities;

namespace DataAggregator.Application.Services.Contracts
{
    public interface INotificationBroker
    {
        Task SendNotificationAsync(NotificationsBroker notification);
    }
}
