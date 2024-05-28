namespace DataAggregator.Infrastructure.Repositories.Base.Contracts
{
    public interface IEventRepository<TCustomerId>
    {
        Task<int> GetEventCountAsync(TCustomerId customerId, DateTime fromDate, DateTime toDate);
    }
}
