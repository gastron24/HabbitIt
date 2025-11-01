using HabbitIt.Domain.Models.Habbit;

namespace HabbitIt.Domain.Collections;

public class HabitCollection
{
    public List<Habit> Habits { get; set; } = new();
}