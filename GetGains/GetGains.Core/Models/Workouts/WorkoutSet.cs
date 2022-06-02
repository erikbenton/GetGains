using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Workouts;

public class WorkoutSet
{
    [Required]
    public int Id { get; set; }

    [Required]
    public WorkoutSetGroup WorkoutSetGroup { get; set; }

    [Required]
    public Exercise? Exercise { get; set; }

    public int? Reps { get; set; }

    public double? Weight { get; set; }

    public double? Time { get; set; }

    public int? WarmUpTime { get; set; }

    public int? CoolDownTime { get; set; }

    public bool IsComplete { get; set; } = false;

    [MaxLength(255)]
    public string? Note { get; set; }

    public WorkoutSet()
    {

    }

    public WorkoutSet(WorkoutSetGroup workoutSetGroup)
    {
        WorkoutSetGroup = workoutSetGroup;
    }
}
