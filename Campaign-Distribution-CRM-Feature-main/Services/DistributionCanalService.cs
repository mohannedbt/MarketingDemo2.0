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

    /// <summary>
    /// Updated Template Method to include templateId.
    /// </summary>
    public async Task<ResponseCampaign> distribuerCampaign(int campaignId, int templateId)
    {
        // 1. Fetch the campaign
        var campaign = await _campaignRepo.findByIdAsync(campaignId);
        
        // 2. NEW: Fetch the specific template chosen by the user in the View
        var chosenTemplate = await _templateRepo.getByIdAsync(templateId);
        
        // 3. Fetch canal configuration
        var canal = await _canalRepo.findCanalByTypeAsync(campaign.CanalType);
        
        // 4. Hook: Build specific content using the chosen template
        var processedTemplate = await construireTemplate(campaign, canal, chosenTemplate); 
        
        // 5. Prepare the response trace
        var response = await preparerDonnees(campaign, processedTemplate);
        
        // 6. Hook: Actual delivery
        await envoyer(response.IdResponse); 
        
        // 7. Log the result
        await enregistrerTrace(response);
        
        return response;
    }

    // Updated abstract method signature to accept the chosen template
    protected abstract Task<Template> construireTemplate(CampaignMarketing campaign, CanalDistribution canal, Template chosenTemplate);
    
    protected abstract Task<ResponseCampaign> preparerDonnees(CampaignMarketing campaign, Template template);
    
    protected abstract Task envoyer(int responseId);

    protected virtual async Task enregistrerTrace(ResponseCampaign response) 
    {
        // Inject the new IResponseRepository into your service constructor
        await _responseRepo.AddResponseAsync(response); 
    }
}