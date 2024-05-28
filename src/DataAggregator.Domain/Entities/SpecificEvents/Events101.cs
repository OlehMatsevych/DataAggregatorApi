using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Domain.Entities.SpecificEvents;

public class Events101 : EventBase<int>
{
    public override decimal Id { get; set; }

    public override int CustomerId { get; set; }

    public override DateTime EventDate { get; set; }

    public override string EventType { get; set; } = null!;
}
