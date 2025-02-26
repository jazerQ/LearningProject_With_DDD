using Application;
using Core.Abstractions.ForRepositories;
using Core.Abstractions.ForServices;
using Core.Enums;
using DataAccess;
using DataAccess.Repository;
using EFcoreLearningProject;
using EFcoreLearningProject.Endpoints;
using EFcoreLearningProject.Extensions;
using EFcoreLearningProject.LogInfo;
using EFcoreLearningProject.Middlewares;
using Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


StaticResetLogInfo.Reset();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileOperationFilter>();
}); ;
builder.Services.AddDbContext<LearningCoursesDbContext>(options => 
{
    options.UseSqlite(builder.Configuration.GetConnectionString("LearningDbContext"));
});
builder.Configuration.AddJsonFile("jwtoptions.json");
builder.Services.Configure<JwtOptions>(builder.Configuration);
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection("AuthorizationOptions"));
builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
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

app.MapCoursesEndpoint();

app.MapLessonEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("get", () => {
    return Results.Ok("Hellp World");
}).RequirePermissions(Permission.Read);

app.MapPost("post", () => 
{
    return Results.Ok("Ты создал что-то");
}).RequirePermissions(Permission.Create);

app.MapPut("put", () =>
  {
      return Results.Ok("Ты обновил что-то");
  }).RequirePermissions(Permission.Update);

app.MapDelete("delete", () =>
{
    return Results.Ok("ты обновил что-то");
});

app.Use(async (context, next) => {
    context.Request.EnableBuffering();
    await next(context);
});

app.UseLog();

app.UseIpInfo();

app.Run();
