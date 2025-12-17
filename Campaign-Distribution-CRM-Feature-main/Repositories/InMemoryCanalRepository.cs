using MarketingDemo.Entities;

namespace MarketingDemo.Repositories;

public class InMemoryCanalRepository : ICanalRepository
{
    // Use the abstract base class to store specialized children
    private static readonly List<CanalDistribution> _canals = new()
    {
        new EmailCanal 
        { 
            IdCanal = "CH_001", 
            Statut = "Active", 
            Objet = "Exclusive Summer Offer",
            ListeEnvoi = new List<string> { "customer1@gmail.com", "customer2@yahoo.fr" }
        },
        new SocialMediaCanal 
        { 
            IdCanal = "CH_002", 
            Statut = "Active", 
            Platform = "LinkedIn", 
            Audience = "B2B Professionals" 
        }
    };

    public Task<CanalDistribution> findCanalByTypeAsync(string type)
    {
        // Realistic lookup based on the specific type name
        var canal = _canals.FirstOrDefault(c => 
            (type == "Email" && c is EmailCanal) || 
            (type == "SocialMedia" && c is SocialMediaCanal));
            
        return Task.FromResult(canal!);
    }

    public Task<IEnumerable<CanalDistribution>> listCanals()
    {
        return Task.FromResult(_canals.AsEnumerable());
    }
}