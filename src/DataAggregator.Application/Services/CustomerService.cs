using DataAggregator.Application.Services.Contracts;
using DataAggregator.Domain.Entities.Base;
using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.RepositoryFactories;
using Microsoft.Extensions.Logging;

namespace DataAggregator.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepositoryFactory _customerRepositoryFactory;
        private readonly EventRepositoryFactory _eventRepositoryFactory;
        private readonly INotificationBroker _notificationBroker;
        private readonly TestContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(CustomerRepositoryFactory customerRepositoryFactory,
                               EventRepositoryFactory eventRepositoryFactory,
                               INotificationBroker notificationBroker,
                               TestContext context,
                               ILogger<CustomerService> logger)
        {
            _customerRepositoryFactory = customerRepositoryFactory;
            _eventRepositoryFactory = eventRepositoryFactory;
            _notificationBroker = notificationBroker;
            _context = context;
            _logger = logger;
        }

        public async Task NotifyQuietCustomersAsync()
        {
            int[] tenantIds = { 101, 145, 2 };
            DateTime fromDate = new DateTime(2024, 04, 01);
            DateTime toDate = new DateTime(2024, 04, 30);

            foreach (var tenantId in tenantIds)
            {
                try
                {
                    _logger.LogInformation($"Processing tenant {tenantId}.");
                    var tenant = await _context.Tenants.FindAsync(tenantId);
                    if (tenant == null)
                    {
                        _logger.LogWarning($"Tenant {tenantId} not found.");
                        continue;
                    }

                    switch (tenantId)
                    {
                        case 101:
                            await NotifyQuietCustomersForTenant<Customer101, int>(tenantId, tenant.OrganisationName, fromDate, toDate);
                            break;
                        case 145:
                            await NotifyQuietCustomersForTenant<Customer145, string>(tenantId, tenant.OrganisationName, fromDate, toDate);
                            break;
                        case 2:
                            await NotifyQuietCustomersForTenant<Customer2, int>(tenantId, tenant.OrganisationName, fromDate, toDate);
                            break;
                        default:
                            _logger.LogWarning($"Tenant {tenantId} is not implemented.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing tenant {tenantId}.");
                }
            }
        }

        private async Task NotifyQuietCustomersForTenant<TCustomer, TCustomerId>(int tenantId, string organisationName, DateTime fromDate, DateTime toDate)
            where TCustomer : CustomerBase<TCustomerId>
        {
            var customerRepo = _customerRepositoryFactory.Create<TCustomer, TCustomerId>(tenantId);
            var eventRepo = _eventRepositoryFactory.Create<TCustomerId>(tenantId);

            var quietCustomers = await customerRepo.GetQuietCustomersAsync(fromDate, toDate);

            foreach (var customer in quietCustomers)
            {
                var eventCount = await eventRepo.GetEventCountAsync(customer.Id, fromDate, toDate);
                if (eventCount < 3)
                {
                    var code = GenerateAuxiliaryClientCode(customer, organisationName);
                    var message = $"Customer {customer.FirstName} {customer.LastName} has been quiet in April. Code: {code}";

                    _logger.LogInformation($"Sending notification to CustomerId: {customer.Id}, Message: {message}");

                    await _notificationBroker.SendNotificationAsync(new Domain.Entities.NotificationsBroker
                    {
                        Email = customer.Email,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        FinHash = code
                    });
                }
            }
        }

        private string GenerateAuxiliaryClientCode<TCustomerId>(CustomerBase<TCustomerId> customer, string organisationName)
        {
            string ReverseSubstring(string input, int start, int length)
            {
                var substring = input.Substring(start, length);
                var charArray = substring.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

            var part1 = ReverseSubstring(customer.FirstName, 1, 3).ToUpper();
            var part2 = ReverseSubstring(customer.LastName, 1, 3).ToUpper();
            var part3 = organisationName.Substring(0, 1).ToUpper();

            return $"{part1}-{part2}-{part3}";
        }
    }

}
