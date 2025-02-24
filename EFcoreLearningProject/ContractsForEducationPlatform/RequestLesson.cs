using System.ComponentModel.DataAnnotations;

namespace EFcoreLearningProject.ContractsForEducationPlatform
{
    public record RequestLesson([Required]string title, string description, [Required]string lessonText);
}
