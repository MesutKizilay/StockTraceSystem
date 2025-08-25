using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace StockTraceSystem.Application.Services.Repositories
{
    public interface IOperationClaimRepository : IAsyncRepository<OperationClaim>
    {
    }
}