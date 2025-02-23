using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.ContractsForEducationPlatform
{
    public record RequestCourse
    (
        [Required]string title,
        [Required]string description,
        decimal price
    );
}
