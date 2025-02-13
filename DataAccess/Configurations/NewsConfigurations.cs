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
    public class NewsConfigurations : IEntityTypeConfiguration<NewsEntity>
    {
        public void Configure(EntityTypeBuilder<NewsEntity> builder)
        {
            builder.HasKey(n => n.Id);
            builder.HasOne(n => n.TitleImage)
                   .WithOne(i => i.News)
                   .HasForeignKey<NewsEntity>(n => n.TitleImageId);
            builder.Property(n => n.Title).IsRequired();
            builder.Property(n => n.TextData).IsRequired();
            builder.Property(n => n.CreatedDate).IsRequired();
            builder.Property(n => n.Views).IsRequired();
        }
    }
}
