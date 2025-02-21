using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.ContractsForEducationPlatform
{
    public record RequestCourse
    (
        [Required]Guid id,
        [Required]string title,
        [Required]string description,
        decimal price
    );
}
