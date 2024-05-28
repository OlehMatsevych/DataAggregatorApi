namespace DataAggregator.Domain.Entities.Base
{
    public abstract class CustomerBase<TCustomerId>
    {
        public abstract TCustomerId Id { get; set; }

        public abstract string FirstName { get; }

        public abstract string LastName { get; }

        public abstract string Email { get; set; }
    }
}
