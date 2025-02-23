using Application;
using Core.Enums;
using Core.Exceptions;
using EFcoreLearningProject.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing;

namespace EFcoreLearningProject.Endpoints
{
    public static class LessonEndpoints
    {
        public static IEndpointRouteBuilder MapLessonEndpoints(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/Lessons/minimal");
            group.MapGet("", Get).RequirePermissions(Permission.Read);
            group.MapPost("", Write).RequirePermissions(Permission.Create);
            return app;
        }
        private static async Task<IResult> Get(Guid courseId, ILessonService lessonService) 
        {
            try
            {
                return Results.Ok(await lessonService.GetLessons(courseId));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return Results.BadRequest(ex.Message);
            }
        }
        private static async Task<IResult> Write(string title, string description, string lessonText, HttpContext httpContext, ILessonService lessonService, ICourseService courseService)
        {
            try
            {
                var userIdClaims = httpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (userIdClaims == null || !Guid.TryParse(userIdClaims.Value, out Guid userId)) return Results.BadRequest("you are not Admin!");
                var courseId = await courseService.GetCourseIdByUserId(userId);
                await lessonService.WriteLesson(courseId, title, description, lessonText);
                return Results.Ok();
            }
            catch (EntityNotFoundException ex) 
            {
                return Results.BadRequest("Oops, you dont have course");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
