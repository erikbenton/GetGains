using GetGains.API.Dtos.Exercises;
using GetGains.Core.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Templates;

public class TemplateSetGroupSummaryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public ExerciseSummaryDto ExerciseSummary { get; set; }

    [Required]
    public int NumberOfSets { get; set; }

    public TemplateSetGroupSummaryDto(TemplateSetGroup tempateSetGroup)
    {
        Id = tempateSetGroup.Id;
        ExerciseSummary = new ExerciseSummaryDto(tempateSetGroup.Exercise);
        NumberOfSets = tempateSetGroup.SetTemplates.Count();
    }
}
