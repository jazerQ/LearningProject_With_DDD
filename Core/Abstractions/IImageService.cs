using Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public interface IImageService
    {
        Task<Result<Image>> CreateImage(IFormFile image, string path);
        int GetNewId();
    }
}