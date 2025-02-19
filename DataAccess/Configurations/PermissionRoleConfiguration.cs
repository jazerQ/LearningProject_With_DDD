using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PermissionRoleConfiguration : IEntityTypeConfiguration<PermissionRoleEntity>
    {
        private readonly AuthorizationOptions _authorization;
        public PermissionRoleConfiguration(AuthorizationOptions authorization)
        {
            _authorization = authorization;
        }
        public void Configure(EntityTypeBuilder<PermissionRoleEntity> builder)
        {
            builder.HasKey(c => new { c.RoleId, c.PermissionId });
            builder.HasData(ParsePermissionRoles());
        }
        private List<PermissionRoleEntity> ParsePermissionRoles() 
        {
            return _authorization.PermissionRole.SelectMany(rp => rp.Permissions
                                                            .Select(r => new PermissionRoleEntity
                                                            {
                                                                RoleId = (int)Enum.Parse<Role>(rp.Role),
                                                                PermissionId = (int)Enum.Parse<Permission>(r)
                                                            })).ToList();
        }
    }
}
