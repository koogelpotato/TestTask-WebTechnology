namespace TestTask_WebTechnology.DTO
{
    public class QueryParameters
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string Name { get; set; } = string.Empty;

        public string OrderBy { get; set; } = "Name";
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
