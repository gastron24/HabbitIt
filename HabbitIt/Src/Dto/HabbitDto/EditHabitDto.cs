using System.ComponentModel.DataAnnotations;

namespace HabbitIt.Dto.HabbitDto;

public class EditHabitDto
{
   [Required]
   public int Id { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Диапазон символов 2 - 50")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Диапазон символов 2 - 50")]
    public string Description { get; set; } = string.Empty;
    
}