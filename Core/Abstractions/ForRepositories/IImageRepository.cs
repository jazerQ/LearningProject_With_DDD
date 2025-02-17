using Core.Models;

namespace Core.Abstractions.ForRepositories
{
    public interface IImageRepository
    {
        int GetNewId();
        Task WriteImagePathIntoDB(int id, int newsId, string fileName);
        Task DeleteImageByNewsId(int newsId);
        Task UpdateImage(Image img);
        Task<int> GetImageIdByNews(int id);
        Task DeleteImage(int id);
    }
}