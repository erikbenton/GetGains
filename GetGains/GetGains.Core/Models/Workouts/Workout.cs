using GetGains.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Workouts;

public class Workout
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public ExerciseCategory Category { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public List<WorkoutSetGroup>? ExerciseGroups { get; set; }

    public Workout(
        string name,
        ExerciseCategory category = ExerciseCategory.Other)
    {
        Name = name;
        Category = category;
    }
}
