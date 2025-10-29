using System.ComponentModel.DataAnnotations;
using HabbitIt.Dto.HabbitDto;

namespace HabbitIt.Domain.Models.Habbit;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class Habbit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? LastCompleteDate { get; set; }
    public bool IsActive { get; set; } = true;

    private readonly List<Habbit> _completionHistory = new();
    public IReadOnlyCollection<Habbit> CompletionHistory => _completionHistory.AsReadOnly();

    public Habbit(string name, string description, DateTime startDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"Имя не может быть пустым!");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"Описание не может быть пустым!");

        Name = name;
        Description = description;
        StartDate = startDate;
    }

    protected Habbit()
    { }
}