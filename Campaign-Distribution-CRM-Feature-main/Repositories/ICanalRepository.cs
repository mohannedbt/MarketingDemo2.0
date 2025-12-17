using MarketingDemo.Entities;
using System.Threading.Tasks;

namespace MarketingDemo.Repositories;

public interface ICanalRepository
{
    Task<CanalDistribution> findCanalByTypeAsync(string type);
}