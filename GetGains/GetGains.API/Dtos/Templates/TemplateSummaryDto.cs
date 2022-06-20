using GetGains.Core.Extensions;
using GetGains.Core.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Templates;

public class TemplateSummaryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public List<TemplateSetGroupSummaryDto> SetSummaries { get; set; }

    public TemplateSummaryDto(Template template)
    {
        Id = template.Id;
        Name = template.Name;
        Category = template.Category.GetLabel();
        SetSummaries = template.GroupTemplates
            .Select(group => new TemplateSetGroupSummaryDto(group))
            .ToList();
    }
}
