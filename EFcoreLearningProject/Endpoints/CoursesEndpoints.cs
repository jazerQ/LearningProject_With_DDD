using Application;
using Core.Enums;
using EFcoreLearningProject.ContractsForEducationPlatform;
using EFcoreLearningProject.Extensions;
using Infrastructure;

namespace EFcoreLearningProject.Endpoints
{
    public static class CoursesEndpoints
    {
        public static IEndpointRouteBuilder MapCoursesEndpoint(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/Courses/minimal");
            group.MapPost("", CoursesEndpoints.Write).RequirePermissions(Permission.Create);
            group.MapGet("", CoursesEndpoints.Get).RequirePermissions(Permission.Read);
            return app;
        }
        public static async Task<IResult> Get(ICourseService courseService) 
        {
            try
            {
                var courses = await courseService.Get();
                return Results.Ok(courses);
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> Write(RequestCourse requestCourse, ICourseService courseService, IAuthorService authorService, HttpContext context)  
        {
            try
            {
                var authorIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (authorIdClaim == null || !Guid.TryParse(authorIdClaim.Value, out Guid authorId)) 
                {
                    return Results.BadRequest("you not admin!");
                }
                await courseService.WriteAsync(requestCourse.id, authorId, requestCourse.title, requestCourse.description, requestCourse.price);
                return Results.NoContent();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
