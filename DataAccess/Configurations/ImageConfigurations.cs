using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ImageConfigurations : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasOne(i => i.News)
                    .WithOne(n => n.TitleImage)
                    .HasForeignKey<ImageEntity>(i => i.NewsId);
        }
    }
}
