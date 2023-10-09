using TestTask_WebTechnology.Enums;
using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology.Interfaces
{
    public interface IRoleRepository
    {
        Task<ICollection<Role>> GetRoles();
        Task<ICollection<Role>> GetUserRoles(int userId);
        Task<bool> AddRolesToUser(int userId, Role role);
        Task<Role> GetRole(int roleId);
        Task<bool> RoleExists(int roleId);
        Task<bool> Save();
    }
}
