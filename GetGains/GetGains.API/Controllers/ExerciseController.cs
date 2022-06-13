using AutoMapper;
using GetGains.API.Dtos.Exercises;
using GetGains.API.Mappers;
using GetGains.Core.Models.Exercises;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseData exerciseContext;

    public ExerciseController(IExerciseData exerciseContext)
    {
        this.exerciseContext = exerciseContext
            ?? throw new ArgumentNullException(nameof(exerciseContext));
    }

    [HttpGet]
    public IActionResult GetAll(bool populateInstructions = false)
    {
        var exercises = exerciseContext.GetAll(populateInstructions);

        var exerciseData = exercises.Select(exer =>
            ExerciseMapper.Map(exer));
        
        return Ok(exerciseData);
    }

    [HttpGet("{id:int}", Name = "GetExercise")]
    public IActionResult GetExercise(int id, bool populateInstructions = true)
    {
        var exercise = exerciseContext.GetExercise(id, populateInstructions);

        if (exercise is null) return NotFound();

        var exerciseData = ExerciseMapper.Map(exercise, populateInstructions);

        return Ok(exerciseData);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ExerciseDto newModel, bool includeData = false)
    {
        var newExercise = ExerciseMapper.Map(newModel);

        exerciseContext.Add(newExercise);

        var savedModel = ExerciseMapper.Map(newExercise);

        return CreatedAtRoute(
            "GetExercise",
            new {id = savedModel.Id},
            includeData ? savedModel : null);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ExerciseDto updatedModel, bool includeData = false)
    {
        var updatedExercise = ExerciseMapper.Map(updatedModel);

        exerciseContext.Update(updatedExercise);

        if (includeData)
        {
            var savedModel = ExerciseMapper.Map(updatedExercise, true);
            return Ok(savedModel);
        }

        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ExerciseDto deletedModel)
    {
        var deletedExercise = ExerciseMapper.Map(deletedModel);

        var isDeleted = exerciseContext.Delete(deletedExercise);

        return isDeleted ? NoContent() : Conflict("Unable to delete exercise");
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteById(int id)
    {
        var exercise = exerciseContext.GetExercise(id);

        if (exercise is null) return NotFound();

        var isDeleted = exerciseContext.Delete(exercise);

        return isDeleted ? NoContent() : Conflict("Unable to delete exercise");
    }
}
