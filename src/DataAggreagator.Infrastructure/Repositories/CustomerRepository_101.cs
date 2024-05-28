using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base;

namespace DataAggregator.Infrastructure.Repositories
{
    public class CustomerRepository_101 : BaseCustomerRepository<Customer101, int>
    {
        public CustomerRepository_101(TestContext context) : base(context) { }
    }
}
