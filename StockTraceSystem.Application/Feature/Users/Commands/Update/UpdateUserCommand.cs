using AutoMapper;
using Core.Security.Entities;
using MediatR;
using StockTraceSystem.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraceSystem.Application.Feature.Users.Commands.Update
{
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);
                await _userRepository.Update(user, cancellationToken);
            }
        }
    }
}