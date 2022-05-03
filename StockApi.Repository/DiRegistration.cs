using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StockApi.Repository
{
    public static class DiRegistration
    {
        public static void ConfigureStockRepository(this IServiceCollection services, string connectionString)
        {

            services.AddDbContextPool<StockContext>(
               options => options.UseSqlServer(connectionString)
            );

        }
    }
}
