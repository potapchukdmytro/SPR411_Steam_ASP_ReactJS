namespace SPR411_SteamClone.DAL.Entities
{
    public class DeveloperEntity : BaseEntity
    {
        public required string Name { get; set; }
        public string? Image { get; set; }

        public List<GameEntity> Games { get; set; } = [];
    }
}
