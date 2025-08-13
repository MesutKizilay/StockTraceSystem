using MediatR;
using StockTraceSystem.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTraceSystem.Application.Feature.Users.Commands.Delete
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }


        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IUserRepository _userRepository;

            public DeleteUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.Get(u => u.Id == request.Id, cancellationToken: cancellationToken);
                user.Status = false;
                await _userRepository.Update(user, cancellationToken);
            }
        }
    }
}