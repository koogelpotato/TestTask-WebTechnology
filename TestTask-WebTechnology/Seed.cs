using TestTask_WebTechnology.Data;
using TestTask_WebTechnology.Enums;
using TestTask_WebTechnology.Models;

namespace TestTask_WebTechnology
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if(!_context.Users.Any())
            {
                var users = new List<User>()
                    {
                        new User()
                        {
                            Name = "John Doe",
                            Age = 30,
                            Email = "john@example.com",
                            Roles = new List<Role>()
                            {
                                new Role { RoleType = RoleTypes.Admin },
                                new Role { RoleType = RoleTypes.User },
                            }
                        },
                        new User()
                        {
                            Name = "Jane Doe",
                            Age = 25,
                            Email = "jane@example.com",
                            Roles = new List<Role>()
                            {
                                new Role { RoleType = RoleTypes.User },
                            }
                        },
                    };
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }
    }
}
