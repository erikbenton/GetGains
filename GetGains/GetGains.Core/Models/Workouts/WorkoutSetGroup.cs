using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Workouts;

public class WorkoutSetGroup
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Workout Workout { get; set; }

    public List<WorkoutSet>? Exercises { get; set; }

    public WorkoutSetGroup()
    {

    }

    public WorkoutSetGroup(Workout workout)
    {
        Workout = workout;
    }

}
