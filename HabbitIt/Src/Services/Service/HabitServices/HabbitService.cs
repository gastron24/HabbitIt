// HabbitIt.Services.Service/HabbitService.cs
using HabbitIt.Application.DataBase;
using HabbitIt.Domain.Exceptions;
using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;
using HabbitIt.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HabbitIt.Services.Service;

public class HabbitService : IHabbitHab
{
    private readonly Db _context;

    public HabbitService(Db context)
    {
        _context = context;
    }

    public async Task<Habit> CreateHabit(CreateHabbitDto createDto)
    {
        if (string.IsNullOrWhiteSpace(createDto.Name) || createDto.Name.Length is < 3 or > 30)
            throw new DomainException("Название должно быть от 3 до 30 символов.");
        
        if (string.IsNullOrWhiteSpace(createDto.Description) || createDto.Description.Length is < 3 or > 250)
            throw new DomainException("Описание должно быть от 3 до 250 символов.");
        
        if (createDto.StartDate > DateTime.UtcNow.Date)
            throw new DomainException("Дата начала не может быть в будущем.");

        var habit = new Habit
        {
            Name = createDto.Name,
            Description = createDto.Description,
            StartDate = createDto.StartDate,
            CreatedAt = DateTime.UtcNow
        };

        _context.Habits.Add(habit);
        await _context.SaveChangesAsync();
        return habit;
    }

    public async Task<Habit> EditHabit(EditHabitDto editDto)
    {
        var habit = await _context.Habits.FindAsync(editDto.Id);
        if (habit == null)
            throw new DomainException("Привычка не найдена.");

        if (string.IsNullOrWhiteSpace(editDto.Name) || editDto.Name.Length is < 3 or > 30)
            throw new DomainException("Название должно быть от 3 до 30 символов.");
        
        if (string.IsNullOrWhiteSpace(editDto.Description) || editDto.Description.Length is < 3 or > 250)
            throw new DomainException("Описание должно быть от 3 до 250 символов.");

        habit.Name = editDto.Name;
        habit.Description = editDto.Description;
        habit.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(); 
        return habit;
    }

    public async Task<Habit?> ShowHabit(int id)
    {
        return await _context.Habits.FindAsync(id);
    }

    public async Task<bool> DeleteHabit(int id)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit == null) return false;

        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();
        return true;
    }
}