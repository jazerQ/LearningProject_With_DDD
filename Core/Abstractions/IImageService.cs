using Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public interface IImageService
    {
        Task<string> SaveImageInPath(IFormFile image, string path);
        Task<Result<Image>> CreateImage(IFormFile image, string path, bool isNewImage);
        int GetNewId();
        Task Delete(int id);
    }
}