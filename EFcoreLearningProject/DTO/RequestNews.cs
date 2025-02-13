using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace EFcoreLearningProject.DTO
{
    public record RequestNews(
        [Required][MaxLength(News.MAX_LENGTH_TITLE)] string Title, 
        [Required] string TextData,
        IFormFile? Image
    );
}
