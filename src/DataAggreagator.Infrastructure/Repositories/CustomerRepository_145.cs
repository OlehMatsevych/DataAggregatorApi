using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base;

namespace DataAggregator.Infrastructure.Repositories
{
    public class CustomerRepository_145 : BaseCustomerRepository<Customer145, string>
    {
        public CustomerRepository_145(TestContext context) : base(context) { }
    }
}
