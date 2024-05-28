using DataAggregator.Domain.Entities.Base;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories;
using DataAggregator.Infrastructure.Repositories.Base.Contracts;

namespace DataAggregator.Infrastructure.RepositoryFactories
{
    public class CustomerRepositoryFactory
    {
        private readonly TestContext _context;

        public CustomerRepositoryFactory(TestContext context)
        {
            _context = context;
        }

        public ICustomerRepository<TCustomer, TCustomerId> Create<TCustomer, TCustomerId>(int tenantId) where TCustomer : CustomerBase<TCustomerId>
        {
            return tenantId switch
            {
                101 => (ICustomerRepository<TCustomer, TCustomerId>)new CustomerRepository_101(_context),
                145 => (ICustomerRepository<TCustomer, TCustomerId>)new CustomerRepository_145(_context),
                2 => (ICustomerRepository<TCustomer, TCustomerId>)new CustomerRepository_2(_context),
                _ => throw new NotImplementedException($"Repository for tenant {tenantId} is not implemented.")
            };
        }
    }
}
