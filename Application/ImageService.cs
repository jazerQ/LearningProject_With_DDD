using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using CSharpFunctionalExtensions;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<Result<Image>> CreateImage(IFormFile image, string path)
        {
            try
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(path, fileName);
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                var titleImage = Image.Create(_imageRepository.GetNewId(), filePath);
                return titleImage;
            }
            catch (Exception ex)
            {
                return Result.Failure<Image>(ex.Message);
            }
        }
        public int GetNewId()
        {
            return _imageRepository.GetNewId();
        }
    }
}
