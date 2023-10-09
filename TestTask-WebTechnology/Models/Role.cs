using TestTask_WebTechnology.Enums;

namespace TestTask_WebTechnology.Models
{
    public class Role
    {
        public int Id { get; set; }
        public RoleTypes RoleType { get; set; } 
        public User User { get; set; }
    }
}