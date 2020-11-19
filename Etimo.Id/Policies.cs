using Etimo.Id.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Etimo.Id
{
    public class Policies
    {
        public const string Admin = RoleNames.Admin;
        public const string User = RoleNames.User;

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}
