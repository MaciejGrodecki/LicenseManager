using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration((cfg => 
            {
                cfg.CreateMap<Room, RoomDto>();
                cfg.CreateMap<LicenseType, LicenseTypeDto>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<License, LicenseDto>();
                cfg.CreateMap<Computer, ComputerDto>();
            })).CreateMapper();
        }
    }
}