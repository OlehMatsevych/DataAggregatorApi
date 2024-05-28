using DataAggregator.Domain.Entities.SpecificEvents;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base;

namespace DataAggregator.Infrastructure.Repositories
{
    public class EventRepository_101 : BaseEventRepository<Events101, int>
    {
        public EventRepository_101(TestContext context) : base(context) { }
    }
}
