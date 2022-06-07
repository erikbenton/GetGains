using GetGains.Core.Models.Workouts;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Workouts;

public class WorkoutSetDto
{
    public int? Id { get; set; }

    [Required]
    public int WorkoutSetGroupId { get; set; }

    [Required]
    public int ExerciseId { get; set; }

    [Required]
    public string ExerciseName { get; set; }

    public int? Reps { get; set; }

    public double? Weight { get; set; }

    public double? Time { get; set; }

    public int? WarmUpTime { get; set; }

    public int? CoolDownTime { get; set; }

    public bool IsComplete { get; set; } = false;

    [MaxLength(255)]
    public string? Note { get; set; }

    public WorkoutSetDto()
    {

    }

    public WorkoutSetDto(WorkoutSet workoutSet)
    {
        Id = workoutSet.Id;
        WorkoutSetGroupId = workoutSet.WorkoutSetGroup.Id;
        ExerciseId = workoutSet.Exercise.Id;
        ExerciseName = workoutSet.Exercise.Name;
        Reps = workoutSet.Reps;
        Weight = workoutSet.Weight;
        Time = workoutSet.Time;
        WarmUpTime = workoutSet.WarmUpTime;
        CoolDownTime = workoutSet.CoolDownTime;
        IsComplete = workoutSet.IsComplete;
        Note = workoutSet.Note;
    }
}
