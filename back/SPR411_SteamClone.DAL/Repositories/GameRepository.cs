using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.DAL.Repositories
{
    public class GameRepository : GenericRepository<GameEntity>
    {
        public GameRepository(AppDbContext context)
            : base(context)
        {
            
        }

        public IQueryable<GameEntity> Games => GetAll();
    }
}
