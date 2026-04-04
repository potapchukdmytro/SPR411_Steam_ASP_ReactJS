using Microsoft.AspNetCore.Identity;

namespace SPR411_SteamClone.DAL.Entities
{
    public class UserRoleEntity : IdentityUserRole<string>
    {

        public UserEntity? User { get; set; }
        public RoleEntity? Role { get; set; }
    }
}
