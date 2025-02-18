using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace Core.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionsEntity> Permissions { get; set; } = [];
        public ICollection<UserEntity> Users { get; set; } = [];

    }
}
