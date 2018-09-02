namespace Web.Models.Common
{
    public class PagesInfo
    {
        public const int DefaultPageSize = 4;

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalRowsCount { get; set; }
    }
}