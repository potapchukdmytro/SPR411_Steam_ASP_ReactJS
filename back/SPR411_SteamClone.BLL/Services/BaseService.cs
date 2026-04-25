using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Dtos.Query;

namespace SPR411_SteamClone.BLL.Services
{
    public abstract class BaseService
    {
        public async Task<(IQueryable<TSrc> query, PaginatedResultDto<TDest> result)> Pagination<TSrc, TDest>(IQueryable<TSrc> items, PaginationDto pagination)
        {
            int totalCount = await items.CountAsync();
            int pageSize = pagination.PageSize < 1 ? 10 : pagination.PageSize;
            int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
            int page = pagination.Page < 1 || pagination.Page > pageCount
                ? 1
                : pagination.Page;

            return (items
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                new PaginatedResultDto<TDest>
                {
                    Page = page,
                    PageCount = pageCount,
                    PageSize = pageSize,
                    TotalCount = totalCount
                });
        }
    }
}
