using System.ComponentModel.DataAnnotations;
using HabbitIt.Dto.HabbitDto;

namespace HabbitIt.Domain.Models.Habbit;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class Habbit
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime? LastCompleteDate { get; private set; }
    public bool IsActive { get; private set; } = true;
    
    private readonly List<Habbit> _completionHistory = new();
    public IReadOnlyCollection<Habbit> CompletionHistory => _completionHistory.AsReadOnly();
    private Habbit() {  }

    public static Habbit Create(string name, string description, DateTime startDate)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new DomainException("Название привычки не может быть пустым.");
        if (name.Length < 3 || name.Length > 30)
            throw new DomainException("Название должно быть от 3 до 30 символов.");
        if(string.IsNullOrWhiteSpace(description))
            throw new DomainException("Описание не может быть пустым.");
        if (description.Length < 3 || description.Length > 250)
            throw new DomainException("Описание должно быть от 3 до 250 символов.");
        if(startDate.Date > DateTime.UtcNow.Date)
            throw new DomainException("Дата начала не может быть в будущем.");

        return new Habbit
        {
            Name = name.Trim(),
            Description = description.Trim(),
            StartDate = startDate.Date,
            IsActive = true,
        };
    }
    
        
    
    
    
}