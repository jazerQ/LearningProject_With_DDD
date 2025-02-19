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
    public class RoleConfigurations : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Permissions).WithMany(u => u.Roles)
                .UsingEntity<PermissionRoleEntity>(
                l => l.HasOne<PermissionsEntity>().WithMany().HasForeignKey(r => r.PermissionId),
                r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(u => u.RoleId)
                );

            var roles = Enum.GetValues<Role>().Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });
            builder.HasData(roles);
        }
    }
}
