using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScope;
        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScope = serviceScopeFactory;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            

            var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
            if (userId == null || !Guid.TryParse(userId.Value, out var id)) 
            {
                return;
            }
            using var scope = _serviceScope.CreateScope();
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = await permissionService.GetPermissionsAsync(id);
            if (permissions.Intersect(requirement.Permissions).Any()) 
            {
                context.Succeed(requirement);
                
            }
        }
    }
}
