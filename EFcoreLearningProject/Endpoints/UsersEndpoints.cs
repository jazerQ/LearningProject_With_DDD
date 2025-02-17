using Application;
using EFcoreLearningProject.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EFcoreLearningProject.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app) 
        {
            
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            return app;
        }
        private static async Task<IResult> Register(RegisterUserRequest registerUserRequest, UserService userService) 
        {
            await userService.Register(registerUserRequest.UserName,
                                 registerUserRequest.Email,
                                 registerUserRequest.Password);
            return Results.Ok();
        }
        private static async Task<IResult> Login(LoginUserRequest loginUserRequest, UserService userService, HttpContext httpContext)  
        {
            var token = await userService.Login(loginUserRequest.email, loginUserRequest.password);
            httpContext.Response.Cookies.Append("itsExactlyNotJwt", token);
            return Results.Ok(token);
        }
    }
}
