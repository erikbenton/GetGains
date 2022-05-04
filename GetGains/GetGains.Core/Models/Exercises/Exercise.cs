using GetGains.Core.Enums;
using GetGains.Core.Models.Instructions;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Exercises;

public class Exercise
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

    public List<Instruction>? Instructions { get; set; }

    public string? Author { get; set; }

    private const string DefaultName = "New Exercise";
}
