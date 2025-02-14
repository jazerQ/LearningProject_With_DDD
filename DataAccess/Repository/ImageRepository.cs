using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Models;
using CSharpFunctionalExtensions;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly LearningCoursesDbContext _context;

        public ImageRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }

        public int GetNewId()
        {
            return _context.Image.Max(i => i.Id) + 1;
        }
        public async Task WriteImagePathIntoDB(int id, int newsId, string fileName)
        {
            ImageEntity imageEntity = new ImageEntity() { Id = id, NewsId = newsId, FileName = fileName };
            await _context.Image.AddAsync(imageEntity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteImageByNewsId(int newsId) 
        {
            await _context.Image.Where(i => i.NewsId == newsId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
        public async Task UpdateImage(Image img) 
        {
            await _context.Image.Where(i => i.Id == img.Id).ExecuteUpdateAsync(i => i.SetProperty(p => p.FileName, img.FileName));
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetImageIdByNews(int id) 
        {
            var imageEntity = await _context.Image.FirstOrDefaultAsync(i => i.NewsId == id);
            if (imageEntity == null) 
            {
                throw new NotImageInDbException($"Not Found Image By NewsId {id}");
            }
            return imageEntity.Id;
            
        }
        public async Task DeleteImage(int id)
        {
            try
            {
                await _context.Image.Where(i => i.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

}
