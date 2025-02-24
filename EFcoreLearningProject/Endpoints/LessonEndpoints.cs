using Application;
using Core.Enums;
using Core.Exceptions;
using EFcoreLearningProject.ContractsForEducationPlatform;
using EFcoreLearningProject.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
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
            group.MapPut("/{id:Guid}", Update).RequirePermissions(Permission.Update);
            group.MapDelete("/{id:Guid}", Delete).RequirePermissions(Permission.Delete);
            group.MapGet("/{id:Guid}", GetById).RequirePermissions(Permission.Read);
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
        private static async Task<IResult> Update(Guid id, RequestLesson requestLesson, ILessonService lessonService, ICourseService courseService, HttpContext context)
        {
            try
            {
                var authorIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (authorIdClaim == null || !Guid.TryParse(authorIdClaim.Value, out Guid authorId)) return Results.BadRequest("you are not Admin!");
                var courseId = await courseService.GetCourseIdByUserId(authorId);
                await lessonService.Update(id, requestLesson.title, requestLesson.description, requestLesson.lessonText, courseId);
                return Results.NoContent();
            }
            catch (Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        private static async Task<IResult> Delete(Guid id, ILessonService lessonService, ICourseService courseService, HttpContext context) 
        {
            try
            {
                var authorClaim = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);
                if (authorClaim == null || !Guid.TryParse(authorClaim.Value, out Guid authorId)) return Results.BadRequest("you are not Admin!");
                var courseId = await courseService.GetCourseIdByUserId(authorId);
                await lessonService.Delete(id, courseId);
                return Results.NoContent();
            }
            catch(Exception ex) 
            {
                return Results.BadRequest(ex.Message);
            }
        }
        private static async Task<IResult> GetById(Guid id, ILessonService lessonService) 
        {
            try
            {
                var result = await lessonService.GetLessonById(id);
                return Results.Ok(result);
            }
            catch (EntityNotFoundException ex) 
            {
                return Results.BadRequest("Извините мы не смогли найти то что вы хотели(");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
