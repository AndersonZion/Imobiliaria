namespace SistemaImobiliario.Shared.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public string? OrderBy { get; set; } = "name";
        public bool Ascending { get; set; } = true;

        public PaginationFilter() { }

        public PaginationFilter(int pageNumber, int pageSize, string? search, string? orderBy, bool ascending)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
            Search = search;
            OrderBy = orderBy;
            Ascending = ascending;
        }
    }
}
