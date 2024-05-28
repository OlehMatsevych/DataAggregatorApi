namespace DataAggregator.Application.Services.Contracts
{
    public interface ICustomerService
    {
        Task NotifyQuietCustomersAsync();
    }
}
