using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LearningCoursesDbContext : DbContext
    {
        public LearningCoursesDbContext(DbContextOptions<LearningCoursesDbContext> options) : base(options) {  }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<ImageEntity> Image { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new AuthorConfigurations());
            modelBuilder.ApplyConfiguration(new CourseConfigurations());
            modelBuilder.ApplyConfiguration(new LessonConfigurations());
            modelBuilder.ApplyConfiguration(new StudentConfigurations());
            modelBuilder.ApplyConfiguration(new ImageConfigurations());
            modelBuilder.ApplyConfiguration(new NewsConfigurations());
            base.OnModelCreating(modelBuilder);
        }
    }
}
