using GetGains.Core.Models.Exercises;
using GetGains.Data.Services;
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
            .Select(e => new ExerciseViewModel(e))
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

        var newExercise = new Exercise()
        {
            Name = newModel.Name,
            BodyPart = newModel.BodyPart,
            Category = newModel.Category,
            Description = newModel.Description,
            MediaUrl = newModel.MediaUrl,
            Instructions = newModel.Instructions,
            Author = newModel.Author,
        };

        _exerciseContext.Add(newExercise);

        return RedirectToAction("Details", new { id = newExercise.Id });
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null)
            return View("Error", new ErrorViewModel());

        var model = new ExerciseViewModel(exercise);

        return View("Details", model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var exercise = _exerciseContext.GetById(id);

        if (exercise is null)
            return View("Error", new ErrorViewModel());

        var model = new ExerciseViewModel(exercise);

        return View("Edit", model);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Edit(ExerciseViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var updatedExercise = new Exercise()
        {
            Id = model.Id,
            Name = model.Name,
            BodyPart = model.BodyPart,
            Category = model.Category,
            Description = model.Description,
            MediaUrl = model.MediaUrl,
            Instructions = model.Instructions,
            Author = model.Author,
        };

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

        var model = new ExerciseViewModel(exercise);

        return View("Delete", model);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Delete(ExerciseViewModel model)
    {
        var exerciseToDelete = new Exercise()
        {
            Id = model.Id,
            Name = model.Name,
            BodyPart = model.BodyPart,
            Category = model.Category,
            Description = model.Description,
            MediaUrl = model.MediaUrl,
            Instructions = model.Instructions,
            Author = model.Author,
        };

        var isDeleted = _exerciseContext.Delete(exerciseToDelete);

        return isDeleted
            ? RedirectToAction("Index")
            : View("Error", new ErrorViewModel());
    }
}
