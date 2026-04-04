using Microsoft.AspNetCore.Identity;

namespace SPR411_SteamClone.DAL.Entities
{
    public class RoleEntity : IdentityRole
    {
        public List<UserRoleEntity> UserRoles { get; set; } = [];
    }
}
