using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;


public class CampagneMarketingService : IMarketing
{
    private readonly ICampaigneRepository _campaignRepo;
    private readonly ITemplateRepository _templateRepo;
    private readonly IEnumerable<DistributionCanalService> _channels;
    private readonly IResponseRepository _responseRepo;

    public CampagneMarketingService(ICampaigneRepository campaignRepo, ITemplateRepository templateRepo,
        IEnumerable<DistributionCanalService> channels, IResponseRepository responseRepo)
    {
        _campaignRepo = campaignRepo;
        _templateRepo = templateRepo;
        _channels = channels;
        _responseRepo = responseRepo;
        
    }

    public async Task<ResponseCampaign> DistribuerCampagne(int campaignId, int templateId, string channelType)
    {
        var strategy = _channels.FirstOrDefault(c => c.GetType().Name.StartsWith(channelType, StringComparison.OrdinalIgnoreCase));
        if (strategy == null) throw new Exception($"Channel {channelType} not supported.");
        
        // Pass both IDs to the distribution strategy
        return await strategy.distribuerCampaign(campaignId, templateId);
    }

    public async Task<IEnumerable<Template>> ListTemplates() => await _templateRepo.getAllAsync();
    public async Task<IEnumerable<CampaignMarketing>> ListCampaigns() => await _campaignRepo.listCampagne();
    
    public async Task CreerCampagne(CampaignMarketing campaign) => await _campaignRepo.AddCampaignAsync(campaign);
    public async Task<IEnumerable<ResponseCampaign>> GetDistributionHistory()
    {
        return await _responseRepo.GetAllResponsesAsync();
    }
}