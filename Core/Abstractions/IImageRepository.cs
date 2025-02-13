
using Core.Models;

namespace DataAccess.Repository
{
    public interface IImageRepository
    {
        int GetNewId();
        Task WriteImagePathIntoDB(int id, int newsId, string fileName);
        Task DeleteImage(int newsId);
        Task UpdateImage(Image img);
    }
}