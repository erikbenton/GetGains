using GetGains.Core.Extensions;
using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Exercises;

public class ExerciseSummaryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string BodyPart { get; set; }

    public ExerciseSummaryDto(Exercise exercise)
    {
        Id = exercise.Id;
        Name = exercise.Name;
        Category = exercise.Category.GetLabel();
        BodyPart = exercise.BodyPart.GetLabel();
    }
}
