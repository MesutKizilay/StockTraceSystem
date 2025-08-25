using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTraceSystem.Application.Services.Repositories;
using StockTraceSystem.Persistence.Contexts;
using StockTraceSystem.Persistence.Repositories;

namespace StockTraceSystem.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StockTraceSystemContext>(options => options.UseSqlServer(configuration.GetConnectionString("MsSqlConnectionString"))
                                                                             .ConfigureWarnings(c => c.Ignore(RelationalEventId.PendingModelChangesWarning)));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddTransient<IOperationClaimRepository, OperationClaimRepository>();

            return services;
        }
    }
}