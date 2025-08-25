using AutoMapper;
using Core.Security.Entities;
using StockTraceSystem.Application.Feature.OperationClaims.Queries.GetList;

namespace StockTraceSystem.Application.Feature.OperationClaims.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OperationClaim,GetListOperationClaimDto>().ReverseMap();
        }
    }
}