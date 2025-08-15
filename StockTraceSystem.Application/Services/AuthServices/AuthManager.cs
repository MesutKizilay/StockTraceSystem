using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using StockTraceSystem.Application.Services.Repositories;

namespace StockTraceSystem.Application.Services.AuthServices
{
    public class AuthManager : IAuthService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            //IList<OperationClaim> operationClaims = await _userOperationClaimRepository
            //    .Query()
            //    .AsNoTracking()
            //    .Where(p => p.UserId == user.Id)
            //    .Select(p => new OperationClaim { Id = p.OperationClaimId, Name = p.OperationClaim.Name })
            //    .ToListAsync();

            IList<OperationClaim> operationClaims = (await _userOperationClaimRepository.GetList(filter: c => c.UserId == user.Id, include: c => c.Include(c => c.OperationClaim)))
                                                                                        .Select(c => new OperationClaim
                                                                                        {
                                                                                            Id = c.OperationClaimId,
                                                                                            Name = c.OperationClaim.Name
                                                                                        }).ToList();

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }
    }
}