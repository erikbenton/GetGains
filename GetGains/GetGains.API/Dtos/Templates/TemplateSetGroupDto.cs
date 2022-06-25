using GetGains.API.Dtos.Exercises;
using GetGains.Core.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Templates;

public class TemplateSetGroupDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public ExerciseSummaryDto ExerciseSummary { get; set; }

    [Required]
    public List<TemplateSetDto> Sets { get; set; }

    public TemplateSetGroupDto(TemplateSetGroup tempateSetGroup)
    {
        Id = tempateSetGroup.Id;
        ExerciseSummary = new ExerciseSummaryDto(tempateSetGroup.Exercise);
        Sets = tempateSetGroup.SetTemplates
            .Select(set => new TemplateSetDto(set))
            .ToList();
    }
}

