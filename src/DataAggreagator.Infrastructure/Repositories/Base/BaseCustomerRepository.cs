using DataAggregator.Domain.Entities.Base;
using DataAggregator.Domain.Entities.SpecificEvents;
using DataAggregator.Infrastructure.Constants;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAggregator.Infrastructure.Repositories.Base
{
    public abstract class BaseCustomerRepository<TCustomer, TCustomerId> : ICustomerRepository<TCustomer, TCustomerId>
        where TCustomer : CustomerBase<TCustomerId>
    {
        protected readonly TestContext _context;

        public BaseCustomerRepository(TestContext context)
        {
            _context = context;
        }

        public async Task<List<TCustomer>> GetQuietCustomersAsync(DateTime fromDate, DateTime toDate)
        {
            var customerEvents = await GetCustomerEventsAsync(fromDate, toDate);

            var quietCustomerIds = customerEvents
                .Where(ce => ce.EventCount < MinimumActivitiesQuiet.MINIMUM_ACTIVITIES_QUIET)
                .Select(ce => ce.CustomerId)
                .ToList();

            var quietCustomers = await _context.Set<TCustomer>()
                .Where(c => quietCustomerIds.Contains(c.Id))
                .ToListAsync();

            return quietCustomers;
        }

        private async Task<List<(TCustomerId CustomerId, int EventCount)>> GetCustomerEventsAsync(DateTime fromDate, DateTime toDate)
        {
            List<(TCustomerId CustomerId, int EventCount)> customerEvents = new();

            if (typeof(TCustomerId) == typeof(int))
            {
                customerEvents.AddRange((IEnumerable<(TCustomerId CustomerId, int EventCount)>)await GetCustomerEventsForIntIdAsync<Events2>(fromDate, toDate));
                customerEvents.AddRange((IEnumerable<(TCustomerId CustomerId, int EventCount)>)await GetCustomerEventsForIntIdAsync<Events101>(fromDate, toDate));
            }
            else if (typeof(TCustomerId) == typeof(string))
            {
                customerEvents.AddRange((IEnumerable<(TCustomerId CustomerId, int EventCount)>)await GetCustomerEventsForStringIdAsync<Events145>(fromDate, toDate));
            }

            return customerEvents;
        }

        private async Task<List<(int CustomerId, int EventCount)>> GetCustomerEventsForIntIdAsync<TEvent>(DateTime fromDate, DateTime toDate)
            where TEvent : EventBase<int>
        {
            var events = await _context.Set<TEvent>()
                .Where(e => e.EventDate >= fromDate && e.EventDate <= toDate)
                .GroupBy(e => e.CustomerId)
                .Select(g => new { CustomerId = g.Key, EventCount = g.Count() })
                .ToListAsync();

            return events.Select(e => (e.CustomerId, e.EventCount)).ToList();
        }

        private async Task<List<(string CustomerId, int EventCount)>> GetCustomerEventsForStringIdAsync<TEvent>(DateTime fromDate, DateTime toDate)
            where TEvent : EventBase<string>
        {
            var events = await _context.Set<TEvent>()
                .Where(e => e.EventDate >= fromDate && e.EventDate <= toDate)
                .GroupBy(e => e.CustomerId)
                .Select(g => new { CustomerId = g.Key, EventCount = g.Count() })
                .ToListAsync();

            return events.Select(e => (e.CustomerId, e.EventCount)).ToList();
        }
    }
}
