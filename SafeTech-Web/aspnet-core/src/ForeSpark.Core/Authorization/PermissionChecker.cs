using Abp.Authorization;
using ForeSpark.Authorization.Roles;
using ForeSpark.Authorization.Users;

namespace ForeSpark.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
