using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;


public interface IMarketing
{
    Task CreerCampagne(CampaignMarketing campaign);
    Task<ResponseCampaign> DistribuerCampagne(int campaignId, string channelType);
    Task<IEnumerable<Template>> ListTemplates();
}

public class CampagneMarketingService : IMarketing
{
    private readonly ICampaigneRepository _campaignRepo;
    private readonly ITemplateRepository _templateRepo;
    private readonly IEnumerable<DistributionCanalService> _channels;

    public CampagneMarketingService(
        ICampaigneRepository campaignRepo, 
        ITemplateRepository templateRepo,
        IEnumerable<DistributionCanalService> channels)
    {
        _campaignRepo = campaignRepo;
        _templateRepo = templateRepo;
        _channels = channels;
    }

    public async Task CreerCampagne(CampaignMarketing campaign)
    {
        // Business Logic: Validate dates as a realistic CRM would
        if (campaign.DateDebut > campaign.DateFin)
            throw new Exception("The start date must be before the end date.");
            
        await _campaignRepo.AddCampaignAsync(campaign);
    }

    public async Task<ResponseCampaign> DistribuerCampagne(int campaignId, string channelType)
    {
        var strategy = _channels.FirstOrDefault(c => c.GetType().Name.StartsWith(channelType));
        if (strategy == null) throw new Exception($"Channel {channelType} not supported.");
        
        return await strategy.distribuerCampaign(campaignId);
    }

    public async Task<IEnumerable<Template>> ListTemplates() => await _templateRepo.getAllAsync();
}