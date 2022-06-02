using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Workouts;

public class Workout
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public List<WorkoutSetGroup>? Sets { get; set; }

    public Workout(string name)
    {
        Name = name;
    }
}
