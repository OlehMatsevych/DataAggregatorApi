using DataAggregator.Domain.Entities.Base;

namespace DataAggregator.Infrastructure.Repositories.Base.Contracts
{
    public interface ICustomerRepository<TCustomer, TCustomerId> where TCustomer : CustomerBase<TCustomerId>
    {
        Task<List<TCustomer>> GetQuietCustomersAsync(DateTime fromDate, DateTime toDate);
    }
}
