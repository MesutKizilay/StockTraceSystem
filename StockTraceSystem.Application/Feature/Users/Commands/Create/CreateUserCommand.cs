using AutoMapper;
using Core.Security.Entities;
using MediatR;
using StockTraceSystem.Application.Services.Repositories;

namespace StockTraceSystem.Application.Feature.Users.Commands.Create
{
    public class CreateUserCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);
                await _userRepository.Add(user, cancellationToken);
            }
        }
    }
}