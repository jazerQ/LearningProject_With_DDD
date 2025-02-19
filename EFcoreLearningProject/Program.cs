using Application;
using Core.Abstractions.ForRepositories;
using Core.Abstractions.ForServices;
using Core.Enums;
using DataAccess;
using DataAccess.Repository;
using EFcoreLearningProject;
using EFcoreLearningProject.Endpoints;
using EFcoreLearningProject.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LearningCoursesDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LearningDbContext"));
});
builder.Configuration.AddJsonFile("jwtoptions.json");
builder.Services.Configure<JwtOptions>(builder.Configuration);
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection("AuthorizationOptions"));
builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
//builder.Services.AddScoped<IImageService, ImageService>();
//builder.Services.AddScoped<INewsService, NewsService>();
//builder.Services.AddScoped<INewsRepository, NewsRepository>();
//builder.Services.AddScoped<IImageRepository, ImageRepository>();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IJwtProvider, JwtProvider>();
//builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<ICourseService, CourseService>();
//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddAllDependency();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions {
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.MapUsersEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapCoursesEndpoint();
app.MapGet("get", () => {
    return Results.Ok("Hellp World");
}).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(Permission.Read, Permission.Create)));
app.Run();
