using StockApi.Repository.Models;

namespace StockApi.Repository.Repositories
{
    public class BrokerRepository : IBrokerRepository
    {
        private readonly StockContext _context;

        public BrokerRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<Broker?> GetById(int brokerId)
        {
            var broker = await _context.Brokers.FindAsync(brokerId);

            return broker;
        }
    }
}
