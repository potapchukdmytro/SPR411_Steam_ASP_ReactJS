namespace SPR411_SteamClone.BLL.Dtos.Query
{
    public class PaginatedResultDto<T>
    {
        public int TotalCount { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public int PageCount { get; set; } = 1;
        public List<T> Items { get; set; } = [];
    }
}
