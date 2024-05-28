using DataAggregator.Domain.Entities.SpecificCustomers;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base;

namespace DataAggregator.Infrastructure.Repositories
{
    public class CustomerRepository_2 : BaseCustomerRepository<Customer2, int>
    {
        public CustomerRepository_2(TestContext context) : base(context) { }
    }
}
