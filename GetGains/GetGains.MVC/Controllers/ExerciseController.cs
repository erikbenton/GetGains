using GetGains.Core.Models.Exercises;
using GetGains.Data.Services;
using GetGains.MVC.Models;
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

    [HttpGet]
    public IActionResult Create()
    {
        var exercise = new Exercise();

        return View("Create", exercise);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Create(Exercise newExercise)
    {
        if (!ModelState.IsValid) return View(newExercise);

        _exerciseContext.Add(newExercise);

        return RedirectToAction("Details", new { id = newExercise.Id });
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null) return View("Error", new ErrorViewModel());

        return View("Details", exercise);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null) return View("Error", new ErrorViewModel());

        return View("Edit", exercise);
    }

    [HttpPost]
    public IActionResult Edit(Exercise exercise)
    {
        if (!ModelState.IsValid) return View(exercise);

        var isSaved = _exerciseContext.Update(exercise);

        return isSaved
            ? RedirectToAction("Details", new { id= exercise.Id })
            : View("Error", new ErrorViewModel());
    }
}
