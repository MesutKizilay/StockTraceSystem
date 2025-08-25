using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using StockTraceSystem.Application.Feature.Auth.Rules;
using StockTraceSystem.Application.Services.AuthServices;
using StockTraceSystem.Application.Services.Repositories;

namespace StockTraceSystem.Application.Feature.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoggedResponse>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;

            public LoginCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
            }

            public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User? user = await _userRepository.Get(filter: u => u.Email == request.UserForLoginDto.Email, cancellationToken: cancellationToken);

                await _authBusinessRules.UserShouldBeExists(user);
                await _authBusinessRules.UserPasswordShouldBeMatch(user, request.UserForLoginDto.Password);
                await _authBusinessRules.UserShouldBeActive(user);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

                LoggedResponse loggedResponse = new LoggedResponse()
                {
                    AccessToken = createdAccessToken
                };

                return loggedResponse;
            }
        }
    }
}