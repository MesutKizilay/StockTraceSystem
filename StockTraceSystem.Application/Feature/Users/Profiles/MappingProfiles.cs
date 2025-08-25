using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using StockTraceSystem.Application.Feature.Users.Commands.Create;
using StockTraceSystem.Application.Feature.Users.Commands.Update;
using StockTraceSystem.Application.Feature.Users.Queries.GetList;

namespace StockTraceSystem.Application.Feature.Users.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Paginate<GetListUserDto>, Paginate<User>>().ReverseMap();
            CreateMap<User, GetListUserDto>()//.ForMember(destinationMember: u => u.OperationClaim, memberOptions: opt => opt.MapFrom(g => g.UserOperationClaims.Select(uop => uop.OperationClaim).FirstOrDefault()))
                                             .ReverseMap();

            CreateMap<UserOperationClaim, UserOperationClaimDto>().ReverseMap();

            //    CreateMap<User, GetListUserDto>().ForMember(d => d.OperationClaim,
            //opt => opt.MapFrom(u =>
            //    u.UserOperationClaims
            //     .Select(x => x.OperationClaim)
            //     .FirstOrDefault()));

            CreateMap<UpdateUserCommand, User>().ReverseMap();

            CreateMap<CreateUserCommand, User>().ReverseMap();
        }
    }
}