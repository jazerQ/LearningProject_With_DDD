using System.Runtime.CompilerServices;
using Application;
using Core.Enums;
using EFcoreLearningProject.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EFcoreLearningProject.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app) 
        {
            
            app.MapPost("register", Register);
            app.MapPost("register-admin", RegisterAdmin);
            app.MapPost("login", Login);
            app.MapDelete("logout", Logout);
            return app;
        }
        private static async Task<IResult> Register(RegisterUserRequest registerUserRequest, UserService userService) 
        {
            await userService.Register(registerUserRequest.UserName,
                                 registerUserRequest.Email,
                                 registerUserRequest.Password,
                                 Role.User);
            return Results.Ok();
        }
        private static async Task<IResult> RegisterAdmin(RegisterUserRequest registerUserRequest, UserService userService) 
        {
            try
            {
                await userService.Register(registerUserRequest.UserName,
                                            registerUserRequest.Email,
                                            registerUserRequest.Password,
                                            Role.Admin);
                return Results.Ok();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        private static async Task<IResult> Login(LoginUserRequest loginUserRequest, UserService userService, HttpContext httpContext)  
        {
            var token = await userService.Login(loginUserRequest.email, loginUserRequest.password);
            httpContext.Response.Cookies.Append("itsExactlyNotJwt", token);
            return Results.Ok(token);
        }
        private static IResult Logout(HttpContext httpContext) 
        {
            httpContext.Response.Cookies.Delete("itsExactlyNotJwt");
            return Results.NoContent();
        }
    }
}
