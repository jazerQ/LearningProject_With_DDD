using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.ContractsForEducationPlatform
{
    public record RequestCourse
    (
        [Required]int id,
        int authorId,
        [Required]string title,
        [Required]string description,
        decimal price
    );
}
