using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public interface IBrokerRepository
    {
        Task<Broker?> GetById(int brokerId);
    }
}