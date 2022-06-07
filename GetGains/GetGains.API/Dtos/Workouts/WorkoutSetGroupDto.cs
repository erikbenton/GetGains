using GetGains.Core.Models.Exercises;
using GetGains.Core.Models.Workouts;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Workouts;

public class WorkoutSetGroupDto
{
    public int? Id { get; set; }

    [Required]
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; }

    public List<WorkoutSetDto>? Sets { get; set; }

    public WorkoutSetGroupDto(WorkoutSetGroup workoutSetGroup, bool populateSets = true)
    {
        Id = workoutSetGroup.Id;
        ExerciseId = workoutSetGroup.Exercise.Id;
        ExerciseName = workoutSetGroup.Exercise.Name;

        if (populateSets)
        {
            Sets = workoutSetGroup.Sets?
                .Select(set => new WorkoutSetDto(set))
                .ToList();
        }
    }
}
