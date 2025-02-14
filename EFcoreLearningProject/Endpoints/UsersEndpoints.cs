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
        private static async Task<IResult> Register() 
        {
            return Results.Ok();
        }
        private static async Task<IResult> Login() 
        {
            return Results.Ok();
        }
    }
}
