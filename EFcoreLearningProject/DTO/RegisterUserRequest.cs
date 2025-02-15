using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.DTO
{
    public record RegisterUserRequest
    (
        [Required] string UserName,
        [Required] string Email,
        [Required] string Password
    );
}
