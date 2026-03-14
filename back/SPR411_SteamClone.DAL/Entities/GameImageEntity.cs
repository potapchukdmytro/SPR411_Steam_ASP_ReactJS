namespace SPR411_SteamClone.DAL.Entities
{
    public class GameImageEntity : BaseEntity
    {
        public required string Name { get; set; }
        public bool IsPreview { get; set; } = false;

        public int GameId { get; set; }
        public GameEntity? Game { get; set; }
    }
}
