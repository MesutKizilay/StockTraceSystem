using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using StockTraceSystem.Application.Feature.Auth.Constants;
using StockTraceSystem.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraceSystem.Application.Feature.Auth.Rules
{
    public class AuthBusinessRules : BaseBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserShouldBeExists(User? user)
        {
            if (user == null)
                await ThrowBusinessException(AuthMessages.UserDoesntExist);
        }

        public async Task UserPasswordShouldBeMatch(User user, string password)
        {
            //User? user = await _userRepository.Get(filter: u => u.Id == id);
            //if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    throw new BusinessException(AuthMessages.PasswordDontMatch);
            if (!user.Password.Equals(password))
            {
                await ThrowBusinessException(AuthMessages.PasswordDoesntMatch);
            }
        }
    }
}