using Core.DTO;
using Core.Models;

namespace Core.Abstractions.ForRepositories
{
    public interface INewsRepository
    {
        int GetNewId();
        Task WriteNewsAsync(News news);
        Task<News> GetNews(int id);
        Task<List<News>> GetAllNews();
        Task DeleteNews(int id);
        Task UpdateNews(NewsUpdateDTO news);
    }
}