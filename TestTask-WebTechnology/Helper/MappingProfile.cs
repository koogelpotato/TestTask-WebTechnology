using AutoMapper;
using TestTask_WebTechnology.DTO;
using TestTask_WebTechnology.Enums;
using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UserDTO, User>().AfterMap((src, dest) =>
            {
                if (dest.Roles == null)
                {
                    dest.Roles = new List<Role>();
                }
                dest.Roles.Add(new Role { RoleType = RoleTypes.User });
            });
            CreateMap<RoleDTO, Role>();
            
        }
    }
}
