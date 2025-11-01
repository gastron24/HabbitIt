// HabbitIt.Domain.Models.Habbit/Habbit.cs
using System.ComponentModel.DataAnnotations;

namespace HabbitIt.Domain.Models.Habbit;

public class Habit
{
    public Habit()
    {
    }
    
    public Habit(string name, string description)
    {
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(250, MinimumLength = 3)]
    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}