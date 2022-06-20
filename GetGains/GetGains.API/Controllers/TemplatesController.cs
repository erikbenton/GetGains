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
}
