using GetGains.Core.Models.Exercises;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Templates;

public class TemplateSetGroup
{
    [Required]
    public int Id { get; set; }

    [Required]
    public Template Template
    {
        get => _template
            ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Template));
        set => _template = value;
    }

    private Template? _template;

    public int TemplateId { get; set; }

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

    public List<TemplateSet> SetTemplates { get; set; } = new List<TemplateSet>();

    public TemplateSetGroup()
    {

    }

    public TemplateSetGroup(Template template, Exercise exercise)
    {
        Template = template;
        TemplateId = template.Id;
        Exercise = exercise;
        ExerciseId = exercise.Id;
    }
}
