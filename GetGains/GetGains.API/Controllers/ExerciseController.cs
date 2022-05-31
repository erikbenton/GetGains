using GetGains.API.Dtos.Exercises;
using GetGains.API.Mappers;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Instructions;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseData exerciseContext;

    public ExerciseController(IExerciseData exerciseContext)
    {
        this.exerciseContext = exerciseContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var exercises = exerciseContext.GetAll();
        var exerciseData = exercises.Select(e => new ExerciseDto(e, true)).ToList();
        return Ok(exerciseData);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById(int id)
    {
        var exercise = exerciseContext.GetById(id);

        if (exercise is null) return BadRequest($"No exercise found with id: {id}");

        var exerciseData = new ExerciseDto(exercise, true);
        return Ok(exerciseData);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ExerciseDto newModel)
    {
        if (!ModelState.IsValid) return BadRequest(newModel.ToString());

        var newExercise = ExerciseMapper.Map(newModel);

        exerciseContext.Add(newExercise);

        var savedModel = ExerciseMapper.Map(newExercise);

        return Ok(savedModel);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ExerciseDto updatedModel)
    {
        if (!ModelState.IsValid) return BadRequest(updatedModel.ToString());

        var updatedExercise = ExerciseMapper.Map(updatedModel);

        exerciseContext.Update(updatedExercise);

        var savedModel = ExerciseMapper.Map(updatedExercise, true);

        return Ok(savedModel);
    }
}
