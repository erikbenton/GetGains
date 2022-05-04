using GetGains.Core.Models.Exercises;
using GetGains.Data.Services;
using GetGains.MVC.Mappers;
using GetGains.MVC.Models;
using GetGains.MVC.Models.Exercises;
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
        var exerciseModels = _exerciseContext
            .GetAll()
            .Select(exercise => ExerciseMapper.Map(exercise))
            .ToList();

        return View("Index", exerciseModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var exerciseModel = new ExerciseViewModel();

        return View("Create", exerciseModel);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Create(ExerciseViewModel newModel)
    {
        if (!ModelState.IsValid)
            return View(newModel);

        var newExercise = ExerciseMapper.Map(newModel);

        _exerciseContext.Add(newExercise);

        return RedirectToAction("Details", new { id = newExercise.Id });
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null)
            return View("Error", new ErrorViewModel());

        var model = ExerciseMapper.Map(exercise);

        return View("Details", model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null)
            return View("Error", new ErrorViewModel());

        var model = ExerciseMapper.Map(exercise);

        return View("Edit", model);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Edit(ExerciseViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var updatedExercise = ExerciseMapper.Map(model);

        var isUpdated = _exerciseContext.Update(updatedExercise);

        return isUpdated
            ? RedirectToAction("Details", new { id= updatedExercise.Id })
            : View("Error", new ErrorViewModel());
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null)
            return View("Error", new ErrorViewModel());

        var model = ExerciseMapper.Map(exercise);

        return View("Delete", model);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Delete(ExerciseViewModel model)
    {
        var exerciseToDelete = ExerciseMapper.Map(model);

        var isDeleted = _exerciseContext.Delete(exerciseToDelete);

        return isDeleted
            ? RedirectToAction("Index")
            : View("Error", new ErrorViewModel());
    }
}
