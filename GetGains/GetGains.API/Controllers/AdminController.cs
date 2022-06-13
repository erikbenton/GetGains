using GetGains.API.Dtos.Exercises;
using GetGains.API.Dtos.Workouts;
using GetGains.API.Mappers;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IWorkoutData workoutContext;
    private readonly IExerciseData exerciseContext;

    public AdminController(
        IWorkoutData workoutContext,
        IExerciseData exerciseContext)
    {
        this.workoutContext = workoutContext;
        this.exerciseContext = exerciseContext;
    }

    [HttpPost("seedExercises")]
    public IActionResult SeedExercises([FromBody] string? password)
    {
        if (password is null) return Unauthorized();

        if (password != "admin") return Unauthorized();

        InMemExerciseData.SeedData(exerciseContext);

        var exercises = exerciseContext.GetAll(true);

        var exerciseData = exercises.Select(e =>
            ExerciseMapper.Map(e, true))
            .ToList();

        return Ok(exerciseData);
    }

    [HttpPost("seedWorkouts")]
    public IActionResult SeedWorkouts([FromBody] string? password)
    {
        if (password is null) return Unauthorized();

        if (password != "admin") return Unauthorized();

        var exercises = exerciseContext.GetAll();

        if (exercises.Count == 0) return BadRequest();

        InMemWorkoutData.SeedData(workoutContext, exercises);

        var workouts = workoutContext.GetAll();

        var workoutModels = workouts
            .Select(w => new WorkoutDto(w, true)).ToList();

        return Ok(workoutModels);
    }
}
