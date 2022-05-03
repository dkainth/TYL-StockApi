using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StockApi.Repository
{
    public static class MigrationManager
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<StockContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
