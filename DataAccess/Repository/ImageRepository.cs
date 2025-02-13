using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
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
        public async Task DeleteImage(int newsId) 
        {
            await _context.Image.Where(i => i.NewsId == newsId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }
        public async Task UpdateImage(Image img) 
        {
            await _context.Image.Where(i => i.Id == img.Id).ExecuteUpdateAsync(i => i.SetProperty(p => p.FileName, img.FileName));
            await _context.SaveChangesAsync();
        }
    }

}
