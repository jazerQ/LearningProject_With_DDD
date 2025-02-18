using Application;
using EFcoreLearningProject.ContractsForEducationPlatform;

namespace EFcoreLearningProject.Endpoints
{
    public static class CoursesEndpoints
    {
        public static IEndpointRouteBuilder MapCoursesEndpoint(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/Courses/minimal");
            group.MapPost("", CoursesEndpoints.Write);
            group.MapGet("", CoursesEndpoints.Get);
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
        public static async Task<IResult> Write(RequestCourse requestCourse, ICourseService courseService, IAuthorService authorService) 
        {
            try
            {
                await courseService.WriteAsync(requestCourse.id, 1, requestCourse.title, requestCourse.description, requestCourse.price);
                return Results.NoContent();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
