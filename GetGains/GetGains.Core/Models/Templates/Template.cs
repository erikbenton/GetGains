using GetGains.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GetGains.Core.Models.Templates;

public class Template
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public ExerciseCategory Category { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public List<TemplateSetGroup> GroupTemplates { get; set; } = new List<TemplateSetGroup>();

    public Template(
        string name,
        ExerciseCategory category = ExerciseCategory.Other)
    {
        Name = name;
        Category = category;
    }
}
