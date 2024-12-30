using AutoMapper;
using Station.Core.Entities;
using Station.Core.Entities.Identities;
using Station.Web.Controllers.Users.Dtos;
using Station.Web.Dtos;
using Station.Web.Dtos.IdentityDtos;


namespace Station.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<ChargeStation, ChargeStationDto>().ReverseMap();

            CreateMap<ConnectorUiStatus, ConnectorUiStatusDto>().ReverseMap();
            CreateMap<ConnectorType, ConnectorTypeDto>().ReverseMap();
            CreateMap<ConnectorStatus, ConnectorStatusDto>().ReverseMap();
            CreateMap<Connector, ConnectorDto>().ReverseMap();


            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<PermissionAction, PermissionActionDto>().ReverseMap();
            CreateMap<PermissionCategory, PermissionCategoryDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();


           
        }
    }
}
