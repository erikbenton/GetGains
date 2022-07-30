using GetGains.API.Controllers;
using GetGains.Data.Services;
using Moq;

namespace GetGains.API.Tests;

public class TemplatesControllerTests
{
    private readonly TemplatesController templatesController;

    public TemplatesControllerTests()
    {
        // Create mock for the data context
        var templateContextMock = new Mock<ITemplateData>();

        templatesController = new TemplatesController(templateContextMock.Object);
    }

    [Fact]
    public void CreateNewTemplate_PostAction_MustReturnCreatedAtResponse()
    {

    }
}