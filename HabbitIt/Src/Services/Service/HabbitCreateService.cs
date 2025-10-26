using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;
using HabbitIt.Services.Interfaces;

namespace HabbitIt.Services.Service;

public class HabbitCreateService : IHabbitHab
{
    public Task<Habbit> CreateHabbit(CreateHabbitDto createDto)
    {
        var habbit = new Habbit({
        
            Description = createDto.Description,
            Name = createDto.Name,
            IsActive = true,
            JustDays = DateTime.Now,
            StartDate = DateTime.UtcNow 
        });
        
        return Task.FromResult(habbit);
    }
    
}
