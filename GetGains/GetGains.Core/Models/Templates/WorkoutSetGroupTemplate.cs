using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Templates;

public class WorkoutSetGroupTemplate
{
    [Required]
    public int Id { get; set; }

    [Required]
    public WorkoutTemplate WorkoutTemplate
    {
        get => _workoutTemplate
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(WorkoutTemplate));
        set => _workoutTemplate = value;
    }

    private WorkoutTemplate? _workoutTemplate;

    [Required]
    public Exercise Exercise
    {
        get => _exercise
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Exercise));
        set => _exercise = value;
    }

    private Exercise? _exercise;

    [Required]
    public int GroupNumber { get; set; }

    public List<WorkoutSetTemplate>? SetTemplates { get; set; }

    public WorkoutSetGroupTemplate()
    {

    }

    public WorkoutSetGroupTemplate(WorkoutTemplate workoutTemplate, Exercise exercise)
    {
        WorkoutTemplate = workoutTemplate;
        Exercise = exercise;
    }
}
