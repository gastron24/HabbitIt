using System.ComponentModel.DataAnnotations;

namespace HabbitIt.Dto.HabbitDto;

public class CreateHabbitDto
{
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = 
        $"Минимальное число символов - 2 , Максимальное число символов - 20")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = 
        $"Минимальное число символов - 2 , Максимальное число символов - 20")]
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    
}