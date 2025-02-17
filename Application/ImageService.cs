using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions.ForRepositories;
using Core.Abstractions.ForServices;
using Core.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<string> SaveImageInPath(IFormFile image, string path) 
        {
            try
            {
                var fileName = image != null ? Path.GetFileName(image.FileName) : "not in path";
                var filePath = image != null ? Path.Combine(path, fileName) : fileName;
                if (image != null)
                {
                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
                return filePath;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Picture saving is failure: {ex.Message}");
                throw;
            }
        }
        public async Task<Result<Image>> CreateImage(IFormFile image, string path, bool isNewImage)
        {
            try
            {
                var filePath = await SaveImageInPath(image, path);
                var titleImage = isNewImage ? Image.Create(_imageRepository.GetNewId(), filePath) : Image.Create(filePath);
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
        public async Task Delete(int id) 
        {
            try
            {
                await _imageRepository.DeleteImage(id);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
