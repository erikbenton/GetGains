using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Templates;

public class TemplateSet
{
    [Required]
    public int Id { get; set; }

    [Required]
    public TemplateSetGroup TemplateSetGroup
    {
        get => _templateSetGroup
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(TemplateSetGroup));
        set => _templateSetGroup = value;
    }

    private TemplateSetGroup? _templateSetGroup;

    public int TemplateSetGroupId { get; set; }

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

    public TemplateSet(TemplateSetGroup templateSetGroup)
    {
        TemplateSetGroup = templateSetGroup;
        TemplateSetGroupId = templateSetGroup.Id;
        Exercise = templateSetGroup.Exercise;
        ExerciseId = templateSetGroup.Exercise.Id;
    }
}
