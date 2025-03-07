﻿using Core.Abstractions.ForRepositories;
using Core.Abstractions.ForServices;
using Core.DTO;
using Core.Exceptions;
using Core.Models;

namespace Application
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;
        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }
        public int GetNewId()
        {
            return _repository.GetNewId();
        }
        public async Task<News> GetNews(int id)
        {
            try
            {
                return await _repository.GetNews(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oh shit, we have error! {ex.Message}");
                throw;
            }
        }
        public async Task WriteNews(News news) 
        {
            try
            {
                await _repository.WriteNewsAsync(news);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Oops, we have some problems {ex.Message}");
                throw ;
            
            }
        }
        public async Task<List<News>> GetAllNews() 
        {
            return await _repository.GetAllNews();
        }
        public async Task DeleteNews(int id) 
        {
            try
            {
                await _repository.DeleteNews(id);
            }
            catch (EntityNotFoundException notFoundException)
            {
                Console.WriteLine($"Oops, we have a small problem {notFoundException.Message}");
                throw;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task UpdateNews(NewsUpdateDTO news) 
        {
            try
            {
                await _repository.UpdateNews(news);
            }
            catch (ImageCreationException imgEx)
            {
                Console.WriteLine($"Oops, while image dont create {imgEx.Message} ");
                throw;
            }
            catch (EntityNotFoundException ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
