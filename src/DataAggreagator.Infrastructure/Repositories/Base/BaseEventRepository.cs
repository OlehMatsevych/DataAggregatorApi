using DataAggregator.Domain.Entities.Base;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAggregator.Infrastructure.Repositories.Base
{
    public abstract class BaseEventRepository<TEvent, TCustomerId> : IEventRepository<TCustomerId>
        where TEvent : EventBase<TCustomerId>
    {
        protected readonly TestContext _context;

        public BaseEventRepository(TestContext context)
        {
            _context = context;
        }

        public async Task<int> GetEventCountAsync(TCustomerId customerId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Set<TEvent>()
                .Where(e => e.CustomerId.Equals(customerId) && e.EventDate >= fromDate && e.EventDate <= toDate)
                .CountAsync();
        }
    }
}
