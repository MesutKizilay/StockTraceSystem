using AutoMapper;
using Core.Application.Request;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockTraceSystem.Application.Services.Repositories;

namespace StockTraceSystem.Application.Feature.Users.Queries.GetList
{
    public class GetListUserQuery : IRequest<Paginate<GetListUserDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, Paginate<GetListUserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<Paginate<GetListUserDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetListWithPaginate(index: request.PageRequest.Index,
                                                                      size: request.PageRequest.Size,
                                                                      cancellationToken: cancellationToken,
                                                                      include: u => u.Include(u => u.UserOperationClaims).ThenInclude(uoc => uoc.OperationClaim));

                var userDtos = _mapper.Map<Paginate<GetListUserDto>>(users);

                return userDtos;
            }
        }
    }
}