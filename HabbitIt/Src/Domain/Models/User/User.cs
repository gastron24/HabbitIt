using System.ComponentModel.DataAnnotations;

namespace HabbitIt.Domain.Models.User;

public class User
{
    [Key]
    public int Id { get; set; }

   [Required]
   [StringLength(50, MinimumLength = 2, ErrorMessage = "Диапазон символов для имени 2 - 50.")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "пароль должен быть от 6 символов!")]
    public string HashedPassword { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public List<Habbit.Habit> Habits { get; set; } = new();

}