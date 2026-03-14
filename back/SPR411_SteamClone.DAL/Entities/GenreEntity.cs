namespace SPR411_SteamClone.DAL.Entities
{
    public class GenreEntity : BaseEntity
    {
        public required string Name { get; set; }

        public List<GameEntity> Games { get; set; } = [];
    }
}
