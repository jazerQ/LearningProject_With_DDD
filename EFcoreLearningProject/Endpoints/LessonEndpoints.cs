using Application;
using Core.Enums;
using EFcoreLearningProject.Extensions;
using Microsoft.AspNetCore.Routing;

namespace EFcoreLearningProject.Endpoints
{
    public static class LessonEndpoints
    {
        public static IEndpointRouteBuilder MapLessonEndpoints(this IEndpointRouteBuilder app) 
        {
            var group = app.MapGroup("/Lessons/minimal");
            group.MapGet("", Get).RequirePermissions(Permission.Read);
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
        private static async Task<IResult> Write(Guid courseId, string title, string description, string lessonText, ILessonService lessonService)
        {
            try
            {
                await lessonService.WriteLesson(courseId, title, description, lessonText);
                return Results.Ok();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
