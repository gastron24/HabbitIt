using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;

namespace HabbitIt.Services.Interfaces;

public interface IHabbitHab
{
    public Task<Habbit> CreateHabbit(CreateHabbitDto createDto);

}