using Microsoft.AspNetCore.Identity;

namespace SPR411_SteamClone.DAL.Entities
{
    public class UserEntity : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }

        public List<UserRoleEntity> UserRoles { get; set; } = [];
    }
}
