using TestTask_WebTechnology.Enums;

namespace TestTask_WebTechnology.QueryParameters
{
    public class RoleQueryParameters
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public RoleTypes RoleType { get; set; }

        public string OrderBy { get; set; } = "RoleType";
        private string sortOrder = "asc";
        public string SortOrder
        {
            get { return sortOrder; }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    sortOrder = value;
                }
            }
        }
    }

}
