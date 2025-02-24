using System.Runtime.CompilerServices;
using Application;
using Core.Abstractions.ForRepositories;
using Core.Abstractions.ForServices;
using DataAccess.Repository;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace EFcoreLearningProject
{
    public static class Injection
    {
        public static IServiceCollection AddAllDependency(this IServiceCollection services) 
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<UserService>();
            services.AddScoped<IMailSenderService, MailSenderService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddScoped<IEmailCheckService, EmailCheckService>();
            return services;
        }
    }
}
