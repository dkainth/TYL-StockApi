using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockApi.Repository.Repositories;

namespace StockApi.Repository
{
    public static class DiRegistration
    {
        public static void ConfigureStockRepository(this IServiceCollection services, string connectionString)
        {

            services.AddDbContextPool<StockContext>(
               options => options.UseSqlServer(connectionString)
            );

            services.AddScoped<IBrokerRepository, BrokerRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
