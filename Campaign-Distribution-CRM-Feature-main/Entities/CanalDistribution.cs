namespace MarketingDemo.Entities;

public abstract class CanalDistribution 
{
    public string IdCanal { get; set; }
    public string Statut { get; set; }
}

public class EmailCanal : CanalDistribution
{
    public List<string> ListeEnvoi { get; set; } = new();
    public string Objet { get; set; } = string.Empty;
}

public class SocialMediaCanal : CanalDistribution
{
    public string Audience { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
}