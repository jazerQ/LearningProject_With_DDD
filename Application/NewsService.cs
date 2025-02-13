using Core.Models;
using DataAccess.Repository;

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
            await _repository.DeleteNews(id);
        }
    }
}
