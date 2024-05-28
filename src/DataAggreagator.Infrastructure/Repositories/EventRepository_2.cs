using DataAggregator.Domain.Entities.SpecificEvents;
using DataAggregator.Infrastructure.Data;
using DataAggregator.Infrastructure.Repositories.Base;

namespace DataAggregator.Infrastructure.Repositories
{
    public class EventRepository_2 : BaseEventRepository<Events2, int>
    {
        public EventRepository_2(TestContext context) : base(context) { }
    }
}
