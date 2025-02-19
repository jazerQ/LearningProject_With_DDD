using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(params Permission[] permission)
        {
            this.Permissions = permission;
        }
        public Permission[] Permissions { get; set; } = [];

    }
}
