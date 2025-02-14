using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.DTO
{
    public class RegisterUserRequest
    {
        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;

    }
}
