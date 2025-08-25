using Core.Persistence.Repositories;
using Core.Security.Entities;
using StockTraceSystem.Application.Services.Repositories;
using StockTraceSystem.Persistence.Contexts;

namespace StockTraceSystem.Persistence.Repositories
{
    public class OperationClaimRepository : EfRepositoryBase<OperationClaim, StockTraceSystemContext>, IOperationClaimRepository
    {
        public OperationClaimRepository(StockTraceSystemContext context) : base(context)
        {
            
        }
    }
}