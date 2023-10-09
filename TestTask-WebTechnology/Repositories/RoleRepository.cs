using Microsoft.EntityFrameworkCore;
using TestTask_WebTechnology.Data;
using TestTask_WebTechnology.Interfaces;
using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRolesToUser(int userId, Role role)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;

            if (!user.Roles.Contains(role))
                return false;

            user.Roles.Add(role);

            return await Save();
        }

        public async Task<Role> GetRole(int roleId)
        {
            return await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Role>> GetRoles()
        {
            return await _context.Roles.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<ICollection<Role>> GetUserRoles(int userId)
        {
            return await _context.Roles.Where(r => r.User.Id == userId).ToListAsync();
        }

        public async Task<bool> RoleExists(int roleId)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
