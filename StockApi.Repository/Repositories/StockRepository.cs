using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockContext _context;

        public StockRepository(StockContext context)
        {
            _context = context;
        }

        public Stock? GetBySymbol(string symbol)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Symbol == symbol);

            return stock;
        }
    }
}
