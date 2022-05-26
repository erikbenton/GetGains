using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers
{
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
            return Ok("Got all");
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var exercise = exerciseContext.GetById(id);

            return Ok($"Got id {id}");
        }
    }
}
