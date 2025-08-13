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
            CreateMap<GetListUserDto, User>().ReverseMap();

            CreateMap<UpdateUserCommand, User>().ReverseMap();

            CreateMap<CreateUserCommand, User>().ReverseMap();
        }
    }
}