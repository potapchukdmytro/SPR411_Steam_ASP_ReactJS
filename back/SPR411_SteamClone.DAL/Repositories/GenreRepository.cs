using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
            : base(context)
        {   
            _context = context;
        }

        public IQueryable<GenreEntity> Genres => GetAll();

        public async Task<GenreEntity?> GetByNameAsync(string name)
        {
            return await _context.Genres
                .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> IsExitsAsync(string name)
        {
            return await GetByNameAsync(name) != null;
        }
    }
}
