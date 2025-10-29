using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;
using HabbitIt.Services.Interfaces;

namespace HabbitIt.Services.Service;

public class HabbitCreateService : IHabbitHab
{
    public async Task<Habbit> CreateHabbit(CreateHabbitDto createDto)
    {
        if (string.IsNullOrWhiteSpace(createDto.Name))
            throw new DomainException("Название привычки не может быть пустым.");
        if (createDto.Name.Length < 3 || createDto.Name.Length > 30)
            throw new DomainException("Название должно быть от 3 до 30 символов.");
        if (string.IsNullOrWhiteSpace(createDto.Description))
            throw new DomainException("Описание не может быть пустым.");
        if (createDto.Description.Length < 3 || createDto.Description.Length > 250)
            throw new DomainException("Описание должно быть от 3 до 250 символов.");
        if (createDto.StartDate > DateTime.UtcNow.Date)
            throw new DomainException("Дата начала не может быть в будущем.");

        var createdHabbit = new Habbit(createDto.Name, createDto.Description, createDto.StartDate);
        return createdHabbit;
    }

}
