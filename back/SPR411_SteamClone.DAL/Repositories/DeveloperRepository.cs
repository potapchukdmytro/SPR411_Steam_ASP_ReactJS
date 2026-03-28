using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Repositories
{
    public class DeveloperRepository
        : GenericRepository<DeveloperEntity>
    {
        public DeveloperRepository(AppDbContext context)
            : base(context)
        {
            
        }

        public IQueryable<DeveloperEntity> Developers => GetAll();

        public async Task<bool> IsExistsAsync(string name)
        {
            return await Developers
                .AsNoTracking()
                .AnyAsync(d => d.Name.ToLower() == name.ToLower());
                
        }

        public async Task<DeveloperEntity?> GetByNameAsync(string name)
        {
            return await Developers
                .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsExistsAsync(string name, params int[] exceptionIds)
        {
            return await Developers
                .AsNoTracking()
                .AnyAsync(d => d.Name.ToLower() == name.ToLower()
                && !exceptionIds.Contains(d.Id));

        }
    }
}
