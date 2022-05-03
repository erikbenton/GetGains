using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.MVC.Controllers;

public class ExerciseController : Controller
{
    private readonly IExerciseData _exerciseContext;

    public ExerciseController(IExerciseData exerciseContext)
    {
        _exerciseContext = exerciseContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var exercises = _exerciseContext.GetAll();

        return View("Index", exercises);
    }
}
