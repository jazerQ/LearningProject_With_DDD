using Core.DTO;
using Core.Models;

namespace Core.Abstractions.ForServices
{
    public interface INewsService
    {
        int GetNewId();
        Task<News> GetNews(int id);
        Task WriteNews(News news);
        Task<List<News>> GetAllNews();
        Task DeleteNews(int id);
        Task UpdateNews(NewsUpdateDTO news);
    }
}