using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Repositories
{
    public class GenericRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            int res = await _context.SaveChangesAsync();
            return res != 0;
        }

        public async Task<bool> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
            int res = await _context.SaveChangesAsync();
            return res != 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            int res = await _context.SaveChangesAsync();
            return res != 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            int res = await _context.SaveChangesAsync();
            return res != 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                int res = await _context.SaveChangesAsync();
                return res != 0;
            }

            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            int res = await _context.SaveChangesAsync();
            return res != 0;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
