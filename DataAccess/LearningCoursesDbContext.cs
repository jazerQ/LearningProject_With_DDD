using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess
{
    public class LearningCoursesDbContext : DbContext
    {
        private readonly IOptions<AuthorizationOptions> _authOptions;
        public LearningCoursesDbContext(DbContextOptions<LearningCoursesDbContext> options, IOptions<AuthorizationOptions> authOptions) : base(options) 
        {
            _authOptions = authOptions;
        }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<ImageEntity> Image { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AuthorConfigurations());
            modelBuilder.ApplyConfiguration(new CourseConfigurations());
            modelBuilder.ApplyConfiguration(new LessonConfigurations());
            modelBuilder.ApplyConfiguration(new StudentConfigurations());
            modelBuilder.ApplyConfiguration(new ImageConfigurations());
            modelBuilder.ApplyConfiguration(new NewsConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new PermissionConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new PermissionRoleConfiguration(_authOptions.Value));
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
