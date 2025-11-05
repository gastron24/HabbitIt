using HabbitIt.Domain.Exceptions;
using HabbitIt.Domain.Models.Habbit;
using HabbitIt.Dto.HabbitDto;
using HabbitIt.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabbitIt.Application.Controllers.HabbitController;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
[AllowAnonymous]
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
        try
        {
            var habit = await _habbitService.CreateHabit(createHabitDto);
            return CreatedAtAction(nameof(CreateHabit), new { id = habit.Id }, habit);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("/edit")]
    public async Task<IActionResult> EditHabit([FromBody] EditHabitDto editHabitDto)
    {
        try
        {
            var habit = await _habbitService.EditHabit(editHabitDto);
            return Ok(habit);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetHabit(int id)
    {
        var habit = await _habbitService.ShowHabit(id);
        if (habit == null)
            return NotFound();
        return Ok(habit);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteHabit(int id)
    {
        var success = await _habbitService.DeleteHabit(id);
        if (!success)
            return NotFound("Привычка не найдена");
        return NoContent();
    }
    
    
     
}   