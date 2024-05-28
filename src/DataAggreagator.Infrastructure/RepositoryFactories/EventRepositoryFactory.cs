using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories;
using DataAggregator.Infrastructure.Repositories.Base.Contracts;

namespace DataAggregator.Infrastructure.RepositoryFactories
{
    public class EventRepositoryFactory
    {
        private readonly TestContext _context;

        public EventRepositoryFactory(TestContext context)
        {
            _context = context;
        }

        public IEventRepository<TCustomerId> Create<TCustomerId>(int tenantId)
        {
            return tenantId switch
            {
                101 => (IEventRepository<TCustomerId>)new EventRepository_101(_context),
                145 => (IEventRepository<TCustomerId>)new EventRepository_145(_context),
                2 => (IEventRepository<TCustomerId>)new EventRepository_2(_context),
                _ => throw new NotImplementedException($"Repository for tenant {tenantId} is not implemented.")
            };
        }
    }
}
