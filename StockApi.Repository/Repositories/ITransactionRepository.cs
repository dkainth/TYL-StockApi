using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public interface ITransactionRepository
    {
        Task Add(Transaction stockTransaction);
        Task SaveChanges();
    }
}