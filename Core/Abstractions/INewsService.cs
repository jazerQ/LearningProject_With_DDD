using Core.Models;

namespace Application
{
    public interface INewsService
    {
        int GetNewId();
        Task<News> GetNews(int id);
        Task WriteNews(News news);
        Task<List<News>> GetAllNews();
        Task DeleteNews(int id);
    }
}