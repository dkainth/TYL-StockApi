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
