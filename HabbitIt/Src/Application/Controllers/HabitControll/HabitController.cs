using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;
using HabbitIt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HabbitIt.Application.Controllers.HabbitController;

[ApiController]
[Route("/api/[controller]")]
public class HabitController : ControllerBase
{
    private readonly IHabbitHab _habbitService;

    public HabitController(IHabbitHab habbitService)
    {
        _habbitService = habbitService;
    }



    [HttpPost("/create")]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabbitDto createHabitDto)
    {
        if (createHabitDto == null)
            return BadRequest("Данные не предоставлены");

        try
        {
            var habbit = await _habbitService.CreateHabbit(createHabitDto);
            return CreatedAtAction(nameof(CreateHabit), habbit);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
     
}   