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

    public class PermissionConfigurations : IEntityTypeConfiguration<PermissionsEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionsEntity> builder)
        {
            builder.HasKey(p => p.Id);
            var permission = Enum.GetValues<Permission>().Select(p => new PermissionsEntity
            {
                Id = (int)p,
                Name = p.ToString()
            });
            builder.HasData(permission);
        }
    }
}
