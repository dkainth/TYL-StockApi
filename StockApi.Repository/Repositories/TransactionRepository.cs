using Microsoft.EntityFrameworkCore;
using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly StockContext _context;

        public TransactionRepository(StockContext context)
        {
            _context = context;
        }

        public Dictionary<string, decimal> FindLastPriceByStock(Stock[] stocks)
        {
            var stockIds = stocks.Select(stock => stock.Id).ToList();

            var transactions = _context.Transactions
                .Include(t => t.Stock)
                .Where(t => t.Stock.Id == stockIds[0])
                .GroupBy(s => s.Stock.Symbol, (k, g) => g.OrderByDescending(e => e.TransactionDate).FirstOrDefault());

            return transactions.ToDictionary(k => k.Stock.Symbol, v => v.PriceGbp);

        }

        public async Task Add(Transaction stockTransaction)
        {
            await _context.Transactions.AddAsync(stockTransaction);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
