using GetGains.API.Dtos.Templates;
using GetGains.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetGains.API.Controllers;

[ApiController]
[Route("templates")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateData templateContext;

    public TemplatesController(ITemplateData templateContext)
    {
        this.templateContext = templateContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemplateSummaryDto>>> GetTemplates()
    {
        var templates = await templateContext.GetTemplatesAsync(true);

        var templateModels = templates
            .Select(template => new TemplateSummaryDto(template)).ToList();

        return Ok(templateModels);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TemplateDto>> GetTemplate(
        int id,
        [FromQuery] bool includeSets = true)
    {
        var template = await templateContext.GetTemplateAsync(id, includeSets);

        if (template is null) return NotFound();

        var templateModel = new TemplateDto(template);

        return Ok(templateModel);
    }
}
