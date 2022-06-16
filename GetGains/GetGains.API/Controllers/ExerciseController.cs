using GetGains.API.Dtos.Exercises;
using GetGains.API.Mappers;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[ApiController]
[Route("exercises")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseData exerciseContext;

    public ExerciseController(IExerciseData exerciseContext)
    {
        this.exerciseContext = exerciseContext
            ?? throw new ArgumentNullException(nameof(exerciseContext));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAllExercises(
        bool populateInstructions = false)
    {
        var exercises = 
            await exerciseContext.GetExercisesAsync(populateInstructions);

        var exerciseData = exercises.Select(exer =>
            ExerciseMapper.Map(exer, populateInstructions));
        
        return Ok(exerciseData);
    }

    [HttpGet("{id:int}", Name = "GetExercise")]
    public async Task<ActionResult<ExerciseDto>> GetExercise(
        int id,
        bool populateInstructions = true)
    {
        var exercise = await exerciseContext.GetExerciseAsync(id, populateInstructions);

        if (exercise is null) return NotFound();

        var exerciseData = ExerciseMapper.Map(exercise, populateInstructions);

        return Ok(exerciseData);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise(
        [FromBody] ExerciseDto newModel,
        bool includeData = false)
    {
        var newExercise = ExerciseMapper.Map(newModel);

        exerciseContext.AddExercise(newExercise);

        await exerciseContext.SaveChangesAsync();

        var savedModel = ExerciseMapper.Map(newExercise);

        return CreatedAtRoute(
            "GetExercise",
            new {id = savedModel.Id},
            includeData ? savedModel : null);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateExercise(
        int id,
        [FromBody] ExerciseDto updatedModel,
        [FromQuery] bool includeData = false)
    {
        var exerciseInDb = await exerciseContext.GetExerciseAsync(id, true);

        if (exerciseInDb is null) return NotFound();

        ExerciseMapper.MapFromTo(updatedModel, exerciseInDb);

        await exerciseContext.SaveChangesAsync();

        if (includeData)
        {
            var updatedExerciseModel = ExerciseMapper.Map(exerciseInDb, true);
            return Ok(updatedExerciseModel);
        }

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteExercise([FromBody] ExerciseDto deletedModel)
    {
        var deletedExercise = ExerciseMapper.Map(deletedModel);

        var isDeleted = exerciseContext.Delete(deletedExercise);

        await exerciseContext.SaveChangesAsync();

        return isDeleted ? NoContent() : Conflict("Unable to delete exercise");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteExerciseById(int id)
    {
        var exercise = await exerciseContext.GetExerciseAsync(id, true);

        if (exercise is null) return NotFound();

        var isDeleted = exerciseContext.Delete(exercise);

        await exerciseContext.SaveChangesAsync();

        return isDeleted ? NoContent() : Conflict("Unable to delete exercise");
    }
}
