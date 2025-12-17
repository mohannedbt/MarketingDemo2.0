namespace MarketingDemo.Entities;

// Entities/CampaignMarketing.cs
public class CampaignMarketing 
{
    public int IdCampagne { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string CanalType { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public bool Statut { get; set; }
    public int TemplateId { get; set; } 
    public string Objectif  { get; set; } = string.Empty;
}