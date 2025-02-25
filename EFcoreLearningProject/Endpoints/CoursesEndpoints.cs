using Application;
using Core.Enums;
using Core.Exceptions;
using EFcoreLearningProject.ContractsForEducationPlatform;
using EFcoreLearningProject.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EFcoreLearningProject.Endpoints
{
    public static class CoursesEndpoints
    {
        public static IEndpointRouteBuilder MapCoursesEndpoint(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/Courses/minimal");
            group.MapPost("", CoursesEndpoints.Write).RequirePermissions(Permission.Create);
            group.MapGet("", CoursesEndpoints.Get).RequirePermissions(Permission.Read);
            group.MapPost("/Join", CoursesEndpoints.JoinToCourse).RequirePermissions(Permission.JoinToCourse);
            group.MapGet("/Managed", CoursesEndpoints.GetManagedCourse).RequirePermissions(Permission.Create);
            group.MapGet("/My", CoursesEndpoints.GetMyCourses).RequirePermissions(Permission.Read);
            group.MapPut("/Managed", CoursesEndpoints.Update).RequirePermissions(Permission.Update);
            group.MapDelete("/Managed", CoursesEndpoints.Delete).RequirePermissions(Permission.Delete);
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
                
                await courseService.WriteAsync(Guid.NewGuid(), authorId, requestCourse.title, requestCourse.description, requestCourse.price);
                return Results.NoContent();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> JoinToCourse(Guid courseId, ICourseService courseService, HttpContext httpContext) 
        {
            try
            {
                var studentIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (studentIdClaim == null || !Guid.TryParse(studentIdClaim.Value, out Guid studentId))
                {
                    return Results.BadRequest("Not found your userId!");
                }
                await courseService.JoinToCourse(courseId, studentId);
                return Results.Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (AlreadyExistException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> GetManagedCourse(ICourseService courseService, HttpContext context) 
        {
            try
            {
                var authorIdClaim = context.User.Claims.FirstOrDefault(u => u.Type == CustomClaims.UserId);
                if (authorIdClaim == null || !Guid.TryParse(authorIdClaim.Value, out Guid authorId)) return Results.BadRequest("you`re not admin!");
                var myCourse = await courseService.GetManagedCourse(authorId);
                return Results.Ok(myCourse);
            }
            catch (EntityNotFoundException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> GetMyCourses(ICourseService courseService, HttpContext context) 
        {
            try
            {
                var studentIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (studentIdClaim == null || !Guid.TryParse(studentIdClaim.Value, out Guid studentId)) return Results.BadRequest("you`re not user!");
                var courseList = await courseService.GetMyCourses(studentId);
                return Results.Ok(courseList);
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> Update(RequestCourse requestCourse, ICourseService courseService, HttpContext context) 
        {
            try
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);

                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId)) return Results.BadRequest("You`re not admin!");

                var courseId = await courseService.GetCourseIdByUserId(userId);

                await courseService.Update(courseId, requestCourse.title, requestCourse.description, requestCourse.price);
                return Results.Ok();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        public static async Task<IResult> Delete(ICourseService courseService, HttpContext context) 
        {
            try
            {

                var userIdClaims = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (userIdClaims == null || !Guid.TryParse(userIdClaims.Value, out Guid userId)) return Results.BadRequest("You`re not admin!");
                var courseId = await courseService.GetCourseIdByUserId(userId);
                await courseService.Delete(courseId);
                return Results.NoContent();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
