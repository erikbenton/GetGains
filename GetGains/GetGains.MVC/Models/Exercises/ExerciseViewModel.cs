using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using GetGains.MVC.Models.Instructions;
using System.ComponentModel.DataAnnotations;

namespace GetGains.MVC.Models.Exercises;

public class ExerciseViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = DefaultName;

    [Required]
    public ExerciseCategory Category { get; set; }

    [Required]
    [Display(Name = "Body Part")]
    public BodyPart BodyPart { get; set; }

    public string? Description { get; set; }

    [Display(Name = "Media URL")]
    public string? MediaUrl { get; set; }

    public List<InstructionViewModel>? Instructions { get; set; }

    public string? Author { get; set; }

    public ExerciseViewModel()
    {

    }

    public ExerciseViewModel(Exercise exercise)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Category = exercise.Category;
        BodyPart = exercise.BodyPart;
        Description = exercise.Description;
        MediaUrl = exercise.MediaUrl;
        Author = exercise.Author;

        Instructions = exercise.Instructions?
            .Select(instruction => new InstructionViewModel(this))
            .ToList();
    }

    private const string DefaultName = "New Exercise";
}
