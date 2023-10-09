namespace TestTask_WebTechnology.Models
{
    public class UserParameters
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; }
    }
}
