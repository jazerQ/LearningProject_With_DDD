using System.Text;
using Core.Enums;
using EFcoreLearningProject.Middlewares;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EFcoreLearningProject.Extensions
{
    public static class ApiExtensions
    {
        public static IApplicationBuilder UseLog(this IApplicationBuilder app) 
        {
            app.UseMiddleware<LoggerMiddleware>();
            return app;
        }
        public static void AddApiAuthentication(this IServiceCollection services, IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),

                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["itsExactlyNotJwt"];

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options => 
                { 
                    options.AddPolicy("Read", policy => policy.Requirements.Add(new PermissionRequirement(Permission.Read)));
                    options.AddPolicy("Create", policy => policy.Requirements.Add(new PermissionRequirement(Permission.Create)));
                    options.AddPolicy("Update", policy => policy.Requirements.Add(new PermissionRequirement(Permission.Update)));
                    options.AddPolicy("Delete", policy => policy.Requirements.Add(new PermissionRequirement(Permission.Delete)));
                    options.AddPolicy("JoinToCourse", policy => policy.Requirements.Add(new PermissionRequirement(Permission.JoinToCourse)));
                });
            
        }

        public static IEndpointConventionBuilder RequirePermissions<TBuilder>(this TBuilder builder, params Permission[] permissions) where TBuilder : IEndpointConventionBuilder 
        {
            return builder.RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirement(permissions)));
           
        }
    }
}
