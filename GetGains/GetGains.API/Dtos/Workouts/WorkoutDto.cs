using GetGains.Core.Enums;
using GetGains.Core.Extensions;
using GetGains.Core.Models.Workouts;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GetGains.API.Dtos.Workouts;

public class WorkoutDto
{
    public int? Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public List<WorkoutSetGroupDto>? ExerciseGroups { get; set; }

    [JsonConstructor]
    public WorkoutDto(string name, string? category = null)
    {
        Name = name;
        Category = category ?? ExerciseCategory.Other.GetLabel();
    }

    public WorkoutDto(Workout workout, bool populateSets = false)
    {
        Id = workout.Id;
        Name = workout.Name;
        Category = workout.Category.GetLabel();
        Description = workout.Description;

        if (populateSets)
        {
            ExerciseGroups = new List<WorkoutSetGroupDto>();
            workout.ExerciseGroups?.ForEach(group =>
            {
                var groupModel = new WorkoutSetGroupDto(group, true);
                ExerciseGroups.Add(groupModel);
            });
        }
    }
}
