using GetGains.API.Dtos.Exercises;
using GetGains.API.Dtos.Templates;
using GetGains.API.Dtos.Workouts;
using GetGains.API.Mappers;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ITemplateData templateContext;
    private readonly IWorkoutData workoutContext;
    private readonly IExerciseData exerciseContext;

    public AdminController(
        ITemplateData templateContext,
        IWorkoutData workoutContext,
        IExerciseData exerciseContext)
    {
        this.templateContext = templateContext;
        this.workoutContext = workoutContext;
        this.exerciseContext = exerciseContext;
    }

    [HttpPost("seedExercises")]
    public async Task<IActionResult> SeedExercises([FromBody] string? password)
    {
        if (password is null) return Unauthorized();

        if (password != "admin") return Unauthorized();

        await ExerciseData.SeedData(exerciseContext);

        var exercises = await exerciseContext.GetExercisesAsync(true);

        var exerciseData = exercises.Select(e =>
            ExerciseMapper.Map(e, true))
            .ToList();

        return Ok(exerciseData);
    }

    [HttpPost("seedTemplates")]
    public async Task<IActionResult> SeedTemplates([FromBody] string? password)
    {
        if (password is null) return Unauthorized();

        if (password != "admin") return Unauthorized();

        templateContext.SeedData();

        await templateContext.SaveChangesAsync();

        var templates = await templateContext.GetTemplatesAsync(true);

        var templateModels = templates
            .Select(template => new TemplateSummaryDto(template)).ToList();

        return Ok(templateModels);
    }

    [HttpPost("seedWorkouts")]
    public async Task<IActionResult> SeedWorkouts([FromBody] string? password)
    {
        if (password is null) return Unauthorized();

        if (password != "admin") return Unauthorized();

        var exercises = await exerciseContext.GetExercisesAsync(true);

        if (exercises.Count() == 0) return BadRequest();

        WorkoutData.SeedData(workoutContext, exercises);

        var workouts = workoutContext.GetAll();

        var workoutModels = workouts
            .Select(w => new WorkoutDto(w, true)).ToList();

        return Ok(workoutModels);
    }
}
