﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Core.Models;
using DataAccess.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Image = Core.Models.Image;

namespace DataAccess.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly LearningCoursesDbContext _context;
        private readonly IImageRepository _imageRepository;
        public NewsRepository(LearningCoursesDbContext context, IImageRepository imageRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
        }
        public int GetNewId()
        {
            return _context.News.Max(n => n.Id) + 1;
        }
        public async Task WriteNewsAsync(News news)
        {
            try
            {
                if (news.TitleImage == null) 
                {
                    throw new Exception("TitleImage is null");
                }
                await _imageRepository.WriteImagePathIntoDB(news.TitleImage.Id, news.Id, news.TitleImage.FileName);
                NewsEntity newsEntity = new NewsEntity() { Id = news.Id, Title = news.Title, TextData = news.TextData, CreatedDate = news.CreatedDate, Views = news.Views, TitleImageId = news.TitleImage.Id };
                await _context.News.AddAsync(newsEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"We Have Some Problems {ex.Message}");
                throw;
            }
        }
        public async Task<News> GetNews(int id)
        {
            var newsEntity = await _context.News.FirstOrDefaultAsync(n => n.Id == id) ?? throw new Exception($"Error! News on the {id} id NOT FOUND");
            IncrementViews(newsEntity);
            await _context.SaveChangesAsync();
            var titleImage = await _context.Image.FirstOrDefaultAsync(i => i.NewsId == id);
            var news = titleImage != null ?
                News.Create(id, newsEntity.Title, newsEntity.TextData, Image.Create(titleImage.Id,titleImage.FileName).Value, newsEntity.CreatedDate, newsEntity.Views)
                : News.Create(id, newsEntity.Title, newsEntity.TextData, null, newsEntity.CreatedDate, newsEntity.Views);
            if (news.IsFailure)
            {
                throw new Exception("Error! While Finding News");
            }

            return news.Value;

        }
        private void IncrementViews(NewsEntity newsEntity)
        {
            newsEntity.Views++;
        }
        public async Task<List<News>> GetAllNews() 
        {
            List<News> listOfNews = await _context.News
                                        .AsNoTracking()
            .OrderBy(n => n.CreatedDate)
            .Select(n => n.TitleImage != null 
            ? News.Create(n.Id, n.Title, n.TextData, Image.Create(n.TitleImageId, n.TitleImage.FileName).Value, n.CreatedDate, n.Views).Value :
              News.Create(n.Id, n.Title, n.TextData, Image.Create(n.TitleImageId, "path not found").Value, n.CreatedDate, n.Views).Value)
                                        .ToListAsync();

            return listOfNews;
        }
        public async Task DeleteNews(int id) 
        {
            await _context.News.Where(n => n.Id == id).ExecuteDeleteAsync();
            await _imageRepository.DeleteImage(id);
            await _context.SaveChangesAsync();
        }
    }
}
