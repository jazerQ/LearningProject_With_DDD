using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure
{
    public class AuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
            if (userId == null || !Guid.TryParse(userId.Value, out var id)) 
            {
                return;
            }
        }
    }
}
