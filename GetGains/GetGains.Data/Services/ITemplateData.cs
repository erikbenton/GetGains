using GetGains.Core.Models.Templates;

namespace GetGains.Data.Services;

public interface ITemplateData
{
    void AddTemplate(Template template);

    void Delete(Template template);

    Task<List<Template>> GetTemplatesAsync(bool populateSets);

    Task<Template?> GetTemplateAsync(int id, bool populateSets);

    Task<bool> SaveChangesAsync();

    void SeedData();
}
