using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.HasMany(u => u.Roles).WithMany(r => r.Users).UsingEntity<UserRoleEntity>
                (
                    u => u.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId),
                    r => r.HasOne<UserEntity>().WithMany().HasForeignKey(u => u.UserId)
                );
        }
    }
}
