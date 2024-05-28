using DataAggregator.Application.Services.Contracts;
using DataAggregator.Domain.Entities;
using DataAggregator.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace DataAggregator.Application.Services
{
    public class NotificationBroker : INotificationBroker
    {
        private readonly TestContext _context;
        private readonly ILogger<NotificationBroker> _logger;

        public NotificationBroker(TestContext context, ILogger<NotificationBroker> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task SendNotificationAsync(NotificationsBroker notification)
        {
            try
            {
                _context.NotificationsBrokers.Add(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Saved notification to DB for Email: {notification.Email} for user: {notification.FirstName} {notification.LastName}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error while saving notification to DB for Email: {ex.Message}");
                throw;
            }

        }
    }
}
