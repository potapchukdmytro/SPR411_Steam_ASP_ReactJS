namespace SPR411_SteamClone.DAL.Entities
{
    public class GameEntity : BaseEntity
    {
        public required string Name { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
        public string? Description { get; set; }

        // Navigation properties and FK
        public int DeveloperId { get; set; }
        public DeveloperEntity? Developer { get; set; }
        public List<GenreEntity> Genres { get; set; } = [];
        public List<GameImageEntity> Images { get; set; } = [];
    }
}
