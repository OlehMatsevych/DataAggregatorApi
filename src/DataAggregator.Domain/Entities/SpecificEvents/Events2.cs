using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Domain.Entities.SpecificEvents;

public partial class Events2 : EventBase<int>
{
    public override decimal Id { get; set; }

    public override int CustomerId { get; set; }

    public override DateTime EventDate { get; set; }

    public override string EventType { get; set; } = null!;
}
