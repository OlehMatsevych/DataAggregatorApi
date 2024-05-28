namespace DataAggregator.Domain.Entities.Base
{
    public abstract class EventBase<TCustomerId>
    {
        public abstract decimal Id { get; set; }

        public abstract TCustomerId CustomerId { get; set; }

        public abstract DateTime EventDate { get; set; }

        public abstract string EventType { get; set; }
    }
}
