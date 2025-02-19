using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace DataAccess
{
    public class AuthorizationOptions
    {
        public PermissionRole[] PermissionRole { get; set; } = []; 
    }
    public class PermissionRole 
    {
        public string Role { get; set; } = string.Empty;
        public string[] Permissions { get; set; } = [];
    }
}
