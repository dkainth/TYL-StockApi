using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public interface ITransactionRepository
    {
        Dictionary<string, decimal> FindLastPriceByStock(Stock[] stocks);

        Task Add(Transaction stockTransaction);

        Task SaveChanges();
    }
}