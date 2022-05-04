using GetGains.Core.Enums;
using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.MVC.Models.Exercises;

public class ExerciseViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public ExerciseCategory Category { get; set; }

    [Required]
    [Display(Name = "Body Part")]
    public BodyPart BodyPart { get; set; }

    public string? Description { get; set; }

    [Display(Name = "Media URL")]
    public string? MediaUrl { get; set; }

    public List<string>? Instructions { get; set; }

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

        // TODO - Will need to deal with Instructions
        // more closely when they are not just strings
        Instructions = exercise.Instructions;
    }
}
