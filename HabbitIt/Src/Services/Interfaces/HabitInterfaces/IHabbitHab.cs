using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;

namespace HabbitIt.Services.Interfaces;

public interface IHabbitHab
{
    public Task<Habit> CreateHabit(CreateHabbitDto createDto);
    public Task<Habit> EditHabit(EditHabitDto editDto);
    
    public Task<Habit> ShowHabit(int id);

    public Task<bool> DeleteHabit(int id);

}