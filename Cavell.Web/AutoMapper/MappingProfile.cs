using AutoMapper;
using Station.Core.Entities;
using Station.Web.Dtos;

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
           
        }
    }
}
