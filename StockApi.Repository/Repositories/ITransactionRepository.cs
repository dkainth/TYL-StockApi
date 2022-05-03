using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public interface ITransactionRepository
    {
        Dictionary<string, decimal> FindLastPriceForStocks(Stock stock);

        Dictionary<string, decimal> FindLastPriceForStocks(Stock[] stocks);

        Task Add(Transaction stockTransaction);

        Task SaveChanges();
    }
}