using GetGains.Core.Extensions;
using GetGains.Core.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace GetGains.API.Dtos.Templates;

public class TemplateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public List<TemplateSetGroupDto> SetGroups { get; set; }

    public TemplateDto(Template template)
    {
        Id = template.Id;
        Name = template.Name;
        Category = template.Category.GetLabel();
        SetGroups = template.GroupTemplates
            .Select(group => new TemplateSetGroupDto(group))
            .ToList();
    }
}
