using MarketingDemo.Entities;
using System.Threading.Tasks;

namespace MarketingDemo.Repositories;

public interface ITemplateRepository
{
    Task createAsync(Template template);
    Task<IEnumerable<Template>> getAllAsync();
}