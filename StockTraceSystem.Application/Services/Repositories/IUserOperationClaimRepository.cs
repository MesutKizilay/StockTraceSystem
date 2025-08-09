using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace StockTraceSystem.Application.Services.Repositories
{
    public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim>
    {
    }
}