using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Workouts;

public class WorkoutSetGroup
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Workout Workout
    {
        get => _workout
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Workout));
        set => _workout = value;
    }

    private Workout? _workout;

    public int WorkoutId { get; set; }

    [Required]
    public Exercise Exercise
    {
        get => _exercise
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Exercise));
        set => _exercise = value;
    }

    private Exercise? _exercise;

    public int ExerciseId { get; set; }

    [Required]
    public int GroupNumber { get; set; }

    public List<WorkoutSet> Sets { get; set; } = new List<WorkoutSet>();

    public WorkoutSetGroup()
    {

    }

    public WorkoutSetGroup(Workout workout, Exercise exercise)
    {
        Workout = workout;
        WorkoutId = workout.Id;
        Exercise = exercise;
        ExerciseId = exercise.Id;
    }

}
