using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Templates;

public class TemplateSet
{
    [Required]
    public int Id { get; set; }

    [Required]
    public TemplateSetGroup WorkoutSetGroupTemplate
    {
        get => _workoutSetGroupTemplate
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(WorkoutSetGroupTemplate));
        set => _workoutSetGroupTemplate = value;
    }

    private TemplateSetGroup? _workoutSetGroupTemplate;

    [Required]
    public Exercise Exercise
    {
        get => _exercise
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Exercise));
        set => _exercise = value;
    }

    private Exercise? _exercise;

    [Required]
    public int SetNumber { get; set; }

    public int? TargetReps { get; set; }

    public double? TargetWeight { get; set; }

    public double? TargetTime { get; set; }

    public int? WarmUpTime { get; set; }

    public int? CoolDownTime { get; set; }

    [MaxLength(255)]
    public string? Note { get; set; }

    public TemplateSet()
    {

    }

    public TemplateSet(TemplateSetGroup workoutSetGroupTemplate)
    {
        WorkoutSetGroupTemplate = workoutSetGroupTemplate;
        Exercise = workoutSetGroupTemplate.Exercise;
    }
}
