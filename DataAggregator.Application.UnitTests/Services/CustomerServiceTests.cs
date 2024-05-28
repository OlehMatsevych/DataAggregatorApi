using DataAggregator.Application.Services;
using DataAggregator.Application.Services.Contracts;
using DataAggregator.Domain.Entities;
using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base.Contracts;
using DataAggregator.Infrastructure.RepositoryFactories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DataAggregator.Application.UnitTests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<CustomerRepositoryFactory> _customerRepositoryFactoryMock;
        private readonly Mock<EventRepositoryFactory> _eventRepositoryFactoryMock;
        private readonly Mock<INotificationBroker> _notificationBrokerMock;
        private readonly Mock<ILogger<CustomerService>> _loggerMock;
        private readonly TestContext _context;

        public CustomerServiceTests()
        {
            _customerRepositoryFactoryMock = new Mock<CustomerRepositoryFactory>(MockBehavior.Strict);
            _eventRepositoryFactoryMock = new Mock<EventRepositoryFactory>(MockBehavior.Strict);
            _notificationBrokerMock = new Mock<INotificationBroker>(MockBehavior.Strict);
            _loggerMock = new Mock<ILogger<CustomerService>>(MockBehavior.Loose);

            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase(databaseName: "CustomerDb")
                .Options;

            _context = new TestContext(options);
        }

        [Fact]
        public async Task NotifyQuietCustomersAsync_ShouldSendNotifications_ForQuietCustomers()
        {
            // Arrange
            var tenantId = 1;
            var organisationName = "Test Org";
            var fromDate = new DateTime(2024, 04, 01);
            var toDate = new DateTime(2024, 04, 30);
            var quietCustomer = new Customer2
            {
                FamilyName = "Matsevych",
                GivenName = "Oleh",
                Email = "test@r.r",
                JobPosition = "test",
                PasswordHash = "hash",
            };

            _context.Tenants.Add(new Tenant { OrganisationName = organisationName });
            _context.Customer2s.Add(quietCustomer);
            await _context.SaveChangesAsync();

            var customerRepositoryMock = new Mock<ICustomerRepository<Customer2, int>>(MockBehavior.Strict);
            customerRepositoryMock.Setup(repo => repo.GetQuietCustomersAsync(fromDate, toDate))
                .ReturnsAsync(new List<Customer2> { quietCustomer });

            var eventRepositoryMock = new Mock<IEventRepository<int>>(MockBehavior.Strict);
            eventRepositoryMock.Setup(repo => repo.GetEventCountAsync(quietCustomer.Id, fromDate, toDate))
                .ReturnsAsync(2);

            _customerRepositoryFactoryMock.Setup(factory => factory.Create<Customer2, int>(tenantId))
                .Returns(customerRepositoryMock.Object);
            _eventRepositoryFactoryMock.Setup(factory => factory.Create<int>(tenantId))
                .Returns(eventRepositoryMock.Object);

            var service = new CustomerService(
                _customerRepositoryFactoryMock.Object,
                _eventRepositoryFactoryMock.Object,
                _notificationBrokerMock.Object,
                _context,
                _loggerMock.Object);

            _notificationBrokerMock.Setup(broker => broker.SendNotificationAsync(It.IsAny<NotificationsBroker>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            await service.NotifyQuietCustomersAsync();

            // Assert
            _notificationBrokerMock.Verify(broker => broker.SendNotificationAsync(It.Is<NotificationsBroker>(nb =>
                nb.Email == quietCustomer.Email &&
                nb.FirstName == quietCustomer.FirstName &&
                nb.LastName == quietCustomer.LastName)), Times.Once);

            customerRepositoryMock.VerifyAll();
            eventRepositoryMock.VerifyAll();
            _customerRepositoryFactoryMock.VerifyAll();
            _eventRepositoryFactoryMock.VerifyAll();
        }
    }
}
