using AutoMapper;
using MediatR;
using StockTraceSystem.Application.Services.Repositories;

namespace StockTraceSystem.Application.Feature.OperationClaims.Queries.GetList
{
    public class GetListOperationClaimQuery : IRequest<List<GetListOperationClaimDto>>
    {
        public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, List<GetListOperationClaimDto>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public GetListOperationClaimQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
            {
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
            }

            public async Task<List<GetListOperationClaimDto>> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
            {
                var userOperationClaims = await _operationClaimRepository.GetList();
                var operationClaimDto = _mapper.Map<List<GetListOperationClaimDto>>(userOperationClaims);

                return operationClaimDto;
            }
        }
    }
}