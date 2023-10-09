using Microsoft.EntityFrameworkCore;
using TestTask_WebTechnology.Data;
using TestTask_WebTechnology.Interfaces;
using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(User user)
        {
            await _context.AddAsync(user);
            return await Save();
        }

        public Task<bool> DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.OrderBy(u => u.Id).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateUser(User user)
        {
             _context.Update(user);
            return await Save();
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
    }
}
