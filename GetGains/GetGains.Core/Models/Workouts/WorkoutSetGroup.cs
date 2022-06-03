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

    public List<WorkoutSet>? Exercises { get; set; }

    public WorkoutSetGroup()
    {

    }

    public WorkoutSetGroup(Workout workout)
    {
        Workout = workout;
    }

}
