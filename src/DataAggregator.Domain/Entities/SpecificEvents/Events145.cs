using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Domain.Entities.SpecificEvents;

public partial class Events145 : EventBase<string>
{
    public override decimal Id { get; set; }

    public override string CustomerId { get; set; } = null!;

    public override DateTime EventDate { get; set; }

    public override string EventType { get; set; } = null!;
}
