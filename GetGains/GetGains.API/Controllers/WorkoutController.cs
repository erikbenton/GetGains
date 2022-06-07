using GetGains.API.Dtos.Workouts;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutData workoutContext;

    public WorkoutController(IWorkoutData workoutContext)
    {
        this.workoutContext = workoutContext;
    }

    [HttpGet]
    public IActionResult GetAll(bool populateSets = false)
    {
        var workouts = workoutContext.GetAll(true);

        var workoutModels = workouts
            .Select(workout => new WorkoutDto(workout, populateSets))
            .ToList();

        return Ok(workoutModels);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var workout = workoutContext.GetById(id);

        if (workout is null) return NotFound();

        var workoutModel = new WorkoutDto(workout, true);

        return Ok(workoutModel);
    }
}
