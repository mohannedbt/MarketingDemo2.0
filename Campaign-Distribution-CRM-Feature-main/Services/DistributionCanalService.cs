using MarketingDemo.Entities;
using MarketingDemo.Repositories;

namespace MarketingDemo.Services;

public abstract class DistributionCanalService
{
    protected readonly ICampaigneRepository _campaignRepo;
    protected readonly ICanalRepository _canalRepo;
    protected readonly ITemplateRepository _templateRepo;
    protected readonly IResponseRepository _responseRepo;

    public DistributionCanalService(
        ICampaigneRepository campaignRepo,
        ICanalRepository canalRepo,
        ITemplateRepository templateRepo,
        IResponseRepository responseRepo)
    {
        _campaignRepo = campaignRepo;
        _canalRepo = canalRepo;
        _templateRepo = templateRepo;
        _responseRepo = responseRepo;
    }

    public async Task<ResponseCampaign> distribuerCampaign(int campaignId, int templateId)
    {
        // 1. Fetch data
        var campaign = await _campaignRepo.findByIdAsync(campaignId);
        var chosenTemplate = await _templateRepo.getByIdAsync(templateId);
        var canal = await _canalRepo.findCanalByTypeAsync(campaign.CanalType);
        
        // 2. Hook: Build specific content
        var processedTemplate = await construireTemplate(campaign, canal, chosenTemplate); 
        
        var response = await preparerDonnees(campaign, processedTemplate);
        
        // 4. Hook: Actual delivery (Simulated)
        await envoyer(response.IdResponse); 
        
        // 5. Internal Log: Save to InMemory repository
        await enregistrerTrace(response);
        
        return response;
    }

    // Abstract methods for Strategy implementations (Email vs SocialMedia)
    protected abstract Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal, Template chosenTemplate);
    protected abstract Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template);
    protected abstract Task envoyer(int responseId);

    // Virtual method: Can be overridden if a specific channel needs special logging
    protected virtual async Task enregistrerTrace(ResponseCampaign response) 
    {
        await _responseRepo.AddResponseAsync(response); 
    }
}