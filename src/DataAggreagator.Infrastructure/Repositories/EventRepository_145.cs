using DataAggregator.Domain.Entities.SpecificEvents;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base;

namespace DataAggregator.Infrastructure.Repositories
{
    public class EventRepository_145 : BaseEventRepository<Events145, string>
    {
        public EventRepository_145(TestContext context) : base(context) { }
    }
}
