using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public interface IStockRepository
    {
        Stock? GetBySymbol(string symbol);
    }
}