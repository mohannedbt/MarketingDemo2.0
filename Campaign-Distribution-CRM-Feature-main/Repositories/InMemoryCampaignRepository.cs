using MarketingDemo.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingDemo.Repositories;

public class InMemoryCampaignRepository : ICampaigneRepository
{
    // Static list to persist data during the application session
    private static readonly List<CampaignMarketing> _campaigns = new()
    {
        new() 
        { 
            IdCampagne = 1, 
            Nom = "Summer Sales 2025", 
            DateDebut = new DateTime(2025, 06, 01), 
            DateFin = new DateTime(2025, 08, 31), 
            Statut = true, 
            CanalType = "Email",
            Objectif = "Increase revenue by 20% through email blast"
        },
        new() 
        { 
            IdCampagne = 2, 
            Nom = "Holiday Product Launch", 
            DateDebut = new DateTime(2025, 12, 01), 
            DateFin = new DateTime(2025, 12, 25), 
            Statut = true, 
            CanalType = "SocialMedia",
            Objectif = "Generate 10k impressions on LinkedIn"
        }
    };

    /// <summary>
    /// Finds a specific campaign by its unique ID (idCampagne)
    /// </summary>
    public Task<CampaignMarketing> findByIdAsync(int id)
    {
        var campaign = _campaigns.FirstOrDefault(c => c.IdCampagne == id);
        return Task.FromResult(campaign);
    }

    /// <summary>
    /// Returns all campaigns for the CRM dashboard
    /// </summary>
    public Task<IEnumerable<CampaignMarketing>> listCampagne()
    {
        return Task.FromResult(_campaigns.AsEnumerable());
    }

    /// <summary>
    /// Adds a new campaign, simulating a database identity increment
    /// </summary>
    public Task AddCampaignAsync(CampaignMarketing campaign)
    {
        int nextId = _campaigns.Any() ? _campaigns.Max(c => c.IdCampagne) + 1 : 1;
        campaign.IdCampagne = nextId;
        _campaigns.Add(campaign);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Modifies an existing campaign (PlanifierCampagne logic in diagram)
    /// </summary>
    public Task ModifyCampgne(CampaignMarketing campaign)
    {
        var existing = _campaigns.FirstOrDefault(c => c.IdCampagne == campaign.IdCampagne);
        if (existing != null)
        {
            existing.Nom = campaign.Nom;
            existing.Statut = campaign.Statut;
            existing.DateDebut = campaign.DateDebut;
            existing.DateFin = campaign.DateFin;
            existing.CanalType = campaign.CanalType;
            existing.Objectif = campaign.Objectif;
        }
        return Task.CompletedTask;
    }
}