using Core.Persistence.Repositories;
using Core.Security.Entities;
using StockTraceSystem.Application.Services.Repositories;
using StockTraceSystem.Persistence.Contexts;

namespace StockTraceSystem.Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, StockTraceSystemContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(StockTraceSystemContext context) : base(context)
        {

        }
    }
}