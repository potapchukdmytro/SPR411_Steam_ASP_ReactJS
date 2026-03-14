namespace SPR411_SteamClone.DAL.Entities
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
