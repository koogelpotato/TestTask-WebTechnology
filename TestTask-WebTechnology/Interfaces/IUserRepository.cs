using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> UserExists(int userId);
        Task<bool> Save();
    }
}
