using MarketingDemo.Entities;
using System.Threading.Tasks;
using MarketingDemo.Entities;

namespace MarketingDemo.Repositories;

public class InMemoryTemplateRepository : ITemplateRepository
{
    private static readonly List<Template> _templates = new()
    {
        new() { Id = 1, Content = "Welcome to our newsletter!", ToWho = "New Subscribers" },
        new() { Id = 2, Content = "Flash Sale: 50% off everything!", ToWho = "Past Customers" }
    };

    public Task<IEnumerable<Template>> getAllAsync()
    {
        
        return Task.FromResult(_templates.AsEnumerable());
    }

    public Task<Template> GetByIdAsync(int id)
    {
        return Task.FromResult(_templates.FirstOrDefault(t => t.Id == id));
    }

    public Task createAsync(Template template)
    {
        template.Id = _templates.Any() ? _templates.Max(t => t.Id) + 1 : 1;
        _templates.Add(template);
        return Task.CompletedTask;
    }
}