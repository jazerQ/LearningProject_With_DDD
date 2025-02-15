using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.DTO
{
    public record LoginUserRequest
    (
        [Required] string email,
        [Required] string password
    );
}
